using AccountService.Infrastructure.Interfaces;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Queries.GetAccountWalletTransactionsByAccount;

public class GetAccountWalletTransactionsByAccountQueryHandler : IRequestHandler<GetAccountWalletTransactionsByAccountQuery, AccountDto>
{
    private readonly IWalletApiService _walletApiService;

    public GetAccountWalletTransactionsByAccountQueryHandler(IWalletApiService walletApiService)
    {
        _walletApiService = walletApiService;
    }

    public async Task<AccountDto> Handle(GetAccountWalletTransactionsByAccountQuery request, CancellationToken cancellationToken)
    {

        var accountTransactions = await _walletApiService.GetAccountWalletTransaction<AccountDto>(request.AccountId, cancellationToken);

        return accountTransactions;
    }
}
