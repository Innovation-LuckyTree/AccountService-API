using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Responses.BonusAccounts;
using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Queries.GetUserBonusCredits;

public class GetUserBonusCreditsQueryHandler : IRequestHandler<GetUserBonusCreditsQuery, BonusAccountBalanceResponse>
{
    private readonly IWalletApiService _walletApiService;

    public GetUserBonusCreditsQueryHandler(IWalletApiService walletApiService)
    {
        _walletApiService = walletApiService;
    }

    public async Task<BonusAccountBalanceResponse> Handle(GetUserBonusCreditsQuery request, CancellationToken cancellationToken)
    {
        var accountCredits = await _walletApiService.GetBonusAccountBalance(request.AccountId, cancellationToken);
        return accountCredits;
    }
}