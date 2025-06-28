using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.Credits.Queries.GetCreditBalance;

public class GetCreditBalanceQueryHandler : IRequestHandler<GetCreditBalanceQuery, AccountBalanceResponse>
{
    private readonly ICoreApiService _coreApiService;
    private readonly IWalletApiService _walletApiService;

    public GetCreditBalanceQueryHandler(ICoreApiService coreApiService, IWalletApiService walletApiService)
    {
        _coreApiService = coreApiService;
        _walletApiService = walletApiService;
    }

    public async Task<AccountBalanceResponse> Handle(GetCreditBalanceQuery request, CancellationToken cancellationToken)
    {
        var accountInfo = await _coreApiService.GetCurrentAccount(cancellationToken);

        var accountCredits = await _walletApiService.GetAccountWalletBalance(accountInfo.AccountCreditId, cancellationToken);
        return accountCredits;
    }
}
