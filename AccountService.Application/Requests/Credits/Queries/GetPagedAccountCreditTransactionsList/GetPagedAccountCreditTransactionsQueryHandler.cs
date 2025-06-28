using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests;
using MediatR;

namespace AccountService.Application.Requests.Credits.Queries.GetPagedAccountCreditTransactionsList;

public class GetPagedAccountCreditTransactionsListQueryHandler : IRequestHandler<GetPagedAccountCreditTransactionsListQuery, AccountDto>
{
    private readonly ICoreApiService _coreApiService;
    private readonly IWalletApiService _walletApiService;

    public GetPagedAccountCreditTransactionsListQueryHandler(ICoreApiService coreApiService, IWalletApiService walletApiService)
    {
        _walletApiService = walletApiService;
        _coreApiService = coreApiService;
    }

    public async Task<AccountDto> Handle(GetPagedAccountCreditTransactionsListQuery request, CancellationToken cancellationToken)
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
