using MediatR;

namespace AccountService.Application.Requests.TransactionRequests.Queries.GetTransactionRequestById;

public record GetTransactionRequestByIdQuery(long Id) : IRequest<TransactionRequestDto>;
