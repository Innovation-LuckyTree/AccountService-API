using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Queries.GetUserAccountCredits;

public class GetUserAccountCreditsQueryHandler : IRequestHandler<GetUserAccountCreditsQuery, AccountBalanceResponse>
{
    private readonly IWalletApiService _walletApiService;

    public GetUserAccountCreditsQueryHandler(IWalletApiService walletApiService)
    {
        _walletApiService = walletApiService;
    }

    public async Task<AccountBalanceResponse> Handle(GetUserAccountCreditsQuery request, CancellationToken cancellationToken)
    {
        var accountCredits = await _walletApiService.GetAccountWalletBalance(request.AccountId, cancellationToken);
        return accountCredits;
    }
}