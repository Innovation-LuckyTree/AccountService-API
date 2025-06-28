using AccountService.Infrastructure.Interfaces;
using MediatR;

namespace AccountService.Application.Requests.Credits.Queries.GetAccountCreditTransactions;

public class GetAccountCreditTransactionsQueryHandler : IRequestHandler<GetAccountCreditTransactionsQuery, AccountDto>
{
    private readonly ICoreApiService _coreApiService;
    private readonly IWalletApiService _walletApiService;

    public GetAccountCreditTransactionsQueryHandler(ICoreApiService coreApiService, IWalletApiService walletApiService)
    {
        _walletApiService = walletApiService;
        _coreApiService = coreApiService;
    }

    public async Task<AccountDto> Handle(GetAccountCreditTransactionsQuery request, CancellationToken cancellationToken)
    {
        var accountInfo = await _coreApiService.GetCurrentAccount(cancellationToken);

        var accountTransactions = await _walletApiService.GetAccountWalletTransaction<AccountDto>(accountInfo.AccountCreditId, cancellationToken);

        return accountTransactions;
    }
}
