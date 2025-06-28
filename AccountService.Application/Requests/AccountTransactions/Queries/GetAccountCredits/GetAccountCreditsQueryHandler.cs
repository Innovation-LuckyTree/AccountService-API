using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Queries.GetAccountCredits;

public class GetAccountCreditsQueryHandler : IRequestHandler<GetAccountCreditsQuery, AccountBalanceResponse>
{
    private readonly ICoreApiService _coreApiService;
    private readonly IWalletApiService _walletApiService;

    public GetAccountCreditsQueryHandler(ICoreApiService coreApiService, IWalletApiService walletApiService)
    {
        _walletApiService = walletApiService;
        _coreApiService = coreApiService;
    }

    public async Task<AccountBalanceResponse> Handle(GetAccountCreditsQuery request, CancellationToken cancellationToken)
    {
        var accountInfo = await _coreApiService.GetCurrentAccount(cancellationToken);

        var accountCredits = await _walletApiService.GetAccountWalletBalance(accountInfo.AccountObjectId, cancellationToken);
        return accountCredits;
    }
}