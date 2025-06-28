using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests.BonusAccounts;
using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Queries.GetPagedBonusWalletTransactions;

public class GetPagedBonusWalletTransactionsQueryHandler(IWalletApiService walletApiService) : IRequestHandler<GetPagedBonusWalletTransactionsQuery, BonusAccountDto>
{
    private readonly IWalletApiService _walletApiService = walletApiService;

    public async Task<BonusAccountDto> Handle(GetPagedBonusWalletTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactionRequest = new PagedBonusTransactionRequest
        {
            AccountId = request.AccountId,
            SearchKey = request.SearchKey,
            Start = request.Start,
            PageSize = request.PageSize,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TransactionType = null
        };

        var accountTransactions = await _walletApiService.GetBonusAccountWalletTransaction<BonusAccountDto>(transactionRequest, cancellationToken);

        return accountTransactions;
    }
}
