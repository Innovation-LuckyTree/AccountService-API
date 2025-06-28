using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests;
using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.Accounts.Queries.GetAccountListBalance
{
    public record GetAccountListBalanceQuery(IEnumerable<Guid> AccountIds) : IRequest<WalletBalancesResponse>;
    public class GetAccountListBalanceQueryHandler(IWalletApiService walletApiService) : IRequestHandler<GetAccountListBalanceQuery, WalletBalancesResponse>
    {
        private readonly IWalletApiService _walletApiService = walletApiService;

        public async Task<WalletBalancesResponse> Handle(GetAccountListBalanceQuery request, CancellationToken cancellationToken)
        {
            var result = await _walletApiService.GetWalletBalances(new WalletBalancesRequest { AccountIds = request.AccountIds }, cancellationToken);
            return result;
        }
    }
}
