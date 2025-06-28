using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Responses;
using AccountService.Infrastructure.WalletApi.Models.Responses.BonusAccounts;
using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Queries.GetBonusAccountCredits;

public class GetBonusAccountCreditsQueryHandler : IRequestHandler<GetBonusAccountCreditsQuery, BonusAccountBalanceResponse>
{
    private readonly ICoreApiService _coreApiService;
    private readonly IWalletApiService _walletApiService;

    public GetBonusAccountCreditsQueryHandler(ICoreApiService coreApiService, IWalletApiService walletApiService)
    {
        _walletApiService = walletApiService;
        _coreApiService = coreApiService;
    }

    public async Task<BonusAccountBalanceResponse> Handle(GetBonusAccountCreditsQuery request, CancellationToken cancellationToken)
    {
        var accountInfo = await _coreApiService.GetCurrentAccount(cancellationToken);

        var accountCredits = await _walletApiService.GetBonusAccountBalance(accountInfo.AccountCreditId, cancellationToken);
        return accountCredits;
    }
}