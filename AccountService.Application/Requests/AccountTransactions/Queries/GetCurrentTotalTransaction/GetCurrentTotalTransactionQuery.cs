using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Queries.GetCurrentTotalTransaction;

public class GetCurrentTotalTransactionQuery : IRequest<AccountTransactionDto>
{
}
