using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Queries.GetPagedAccountWalletTransactions;

public class GetPagedAccountWalletTransactionsQueryHandler : IRequestHandler<GetPagedAccountWalletTransactionsQuery, AccountDto>
{
    private readonly ICoreApiService _coreApiService;
    private readonly IWalletApiService _walletApiService;

    public GetPagedAccountWalletTransactionsQueryHandler(ICoreApiService coreApiService, IWalletApiService walletApiService)
    {
        _walletApiService = walletApiService;
        _coreApiService = coreApiService;
    }

    public async Task<AccountDto> Handle(GetPagedAccountWalletTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactionRequest = new PagedAccountTransactionRequest
        {
            AccountId = request.AccountId,
            SearchKey = request.SearchKey,
            Start = request.Start,
            PageSize = request.PageSize,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TransactionType = null
        };

        var accountTransactions = await _walletApiService.GetAccountWalletTransaction<AccountDto>(transactionRequest, cancellationToken);

        return accountTransactions;
    }
}
