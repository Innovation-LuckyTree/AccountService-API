using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Queries.GetAccountWalletTransactionsByAccount;

public record GetAccountWalletTransactionsByAccountQuery(Guid AccountId) : IRequest<AccountDto>;

