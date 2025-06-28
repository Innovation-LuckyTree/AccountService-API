using AccountService.Infrastructure.Interfaces;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Queries.GetAccountWalletTransactions;

public class GetAccountWalletTransactionsQueryHandler : IRequestHandler<GetAccountWalletTransactionsQuery, AccountDto>
{
    private readonly ICoreApiService _coreApiService;
    private readonly IWalletApiService _walletApiService;

    public GetAccountWalletTransactionsQueryHandler(ICoreApiService coreApiService, IWalletApiService walletApiService)
    {
        _walletApiService = walletApiService;
        _coreApiService = coreApiService;
    }

    public async Task<AccountDto> Handle(GetAccountWalletTransactionsQuery request, CancellationToken cancellationToken)
    {
        var accountInfo = await _coreApiService.GetCurrentAccount(cancellationToken);

        var accountTransactions = await _walletApiService.GetAccountWalletTransaction<AccountDto>(accountInfo.AccountObjectId, cancellationToken);

        return accountTransactions;
    }
}
