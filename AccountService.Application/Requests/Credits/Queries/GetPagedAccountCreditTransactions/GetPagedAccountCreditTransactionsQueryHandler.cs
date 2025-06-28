using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests;
using MediatR;

namespace AccountService.Application.Requests.Credits.Queries.GetPagedAccountCreditTransactions;

public class GetPagedAccountCreditTransactionsQueryHandler(IWalletApiService walletApiService) : IRequestHandler<GetPagedAccountCreditTransactionsQuery, AccountDto>
{
    private readonly IWalletApiService _walletApiService = walletApiService;

    public async Task<AccountDto> Handle(GetPagedAccountCreditTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactionRequest = new PagedAccountTransactionRequest
        {
            AccountId = request.AccountId,
            SearchKey = request.SearchKey,
            Start = request.Start,
            PageSize = request.PageSize,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TransactionType = request.TransactionType
        };

        var accountTransactions = await _walletApiService.GetAccountWalletTransaction<AccountDto>(transactionRequest, cancellationToken);

        return accountTransactions;
    }
}
