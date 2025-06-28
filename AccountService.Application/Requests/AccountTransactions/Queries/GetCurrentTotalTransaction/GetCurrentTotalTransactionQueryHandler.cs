using AccountService.Application.Requests.AccountTransactions.Queries.GetAccountCredits;
using AccountService.Application.Requests.AccountTransactions.Queries.GetPagedAccountWalletTransactions;
using AccountService.Application.Requests.Credits.Queries.GetCreditBalance;
using AccountService.Application.Requests.Credits.Queries.GetPagedAccountCreditTransactions;
using AccountService.Common.Enums;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Queries.GetCurrentTotalTransaction;

public class GetCurrentTotalTransactionQueryHandler(IMediator mediator) : IRequestHandler<GetCurrentTotalTransactionQuery, AccountTransactionDto>
{
    private readonly string CASHIN_REFERENCE = "ACCOUNT-CASH-IN";
    private readonly string WITHDRAW_REFERENCE = "ACCOUNT-WITHDRAW";
    private readonly string BET_REFERENCE = "ACCOUNT-BET";

    private readonly IMediator _mediator = mediator;

    public async Task<AccountTransactionDto> Handle(GetCurrentTotalTransactionQuery request, CancellationToken cancellationToken)
    {
        var response = new AccountTransactionDto();

        var dNow = DateTime.Now.Date;

        var accountTransactionsRequest = new GetPagedAccountWalletTransactionsQuery
        {
            SearchKey = "",
            TransactionType = null,
            Start = 0,
            PageSize = 1000,
            StartDate = dNow,
            EndDate = dNow.AddDays(1)
        };

        var accountTransactionsResult = await _mediator.Send(accountTransactionsRequest, cancellationToken);

        var walletBalance = await _mediator.Send(new GetAccountCreditsQuery(), cancellationToken);
        var creditBalance = await _mediator.Send(new GetCreditBalanceQuery(), cancellationToken);
        var totalBet = await GetCurrentTotalBet(cancellationToken);

        var cashinTransactions = accountTransactionsResult.Transactions.Where(o => o.TransactionReference.Equals(CASHIN_REFERENCE, StringComparison.OrdinalIgnoreCase));
        var cashoutTransactions = accountTransactionsResult.Transactions.Where(o => o.TransactionReference.Equals(WITHDRAW_REFERENCE, StringComparison.OrdinalIgnoreCase));

        response.TotalBetAmount = totalBet;
        response.TotalCashIn = cashinTransactions?.Sum(o => o.Amount) ?? 0;
        response.TotalCashInCount = cashinTransactions?.Count() ?? 0;
        response.TotalCashOut = cashoutTransactions?.Sum(o => o.Amount) ?? 0;
        response.TotalCashOutCount = cashoutTransactions?.Count() ?? 0;
        response.WalletBalance = walletBalance;
        response.CreditBalance = creditBalance;
        response.Date = dNow;

        return response;
    }

    private async Task<decimal> GetCurrentTotalBet(CancellationToken cancellationToken)
    {
        var dNow = DateTime.Now.Date;

        var betRequest = new GetPagedAccountCreditTransactionsQuery
        {
            SearchKey = BET_REFERENCE,
            TransactionType = (int)AccountTransactionTypes.Credit,
            Start = 0,
            PageSize = 1000,
            StartDate = dNow,
            EndDate = dNow.AddDays(1)
        };

        var betsresponse = await _mediator.Send(betRequest, cancellationToken);

        return betsresponse?.Transactions?.Sum(o => o.Amount) ?? 0;
    }
}
