using MediatR;

namespace AccountService.Application.Requests.Transactions.Queries.GetUnprocessedTransaction;

public class GetUnprocessedTransactionQuery : IRequest<UnprocessedTransactionVm>;



