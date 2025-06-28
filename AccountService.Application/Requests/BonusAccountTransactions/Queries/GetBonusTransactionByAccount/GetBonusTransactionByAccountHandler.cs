using AccountService.Infrastructure.Interfaces;
using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Queries.GetBonusTransactionByAccount;

public class GetBonusTransactionByAccountQueryHandler(IWalletApiService walletApiService) : IRequestHandler<GetBonusTransactionByAccountQuery, BonusAccountDto>
{
    private readonly IWalletApiService _walletApiService = walletApiService;

    public async Task<BonusAccountDto> Handle(GetBonusTransactionByAccountQuery request, CancellationToken cancellationToken)
    {
        var accountTransactions = await _walletApiService.GetBonusAccountTransaction<BonusAccountDto>(request.AccountId, cancellationToken);

        return accountTransactions;
    }
}
