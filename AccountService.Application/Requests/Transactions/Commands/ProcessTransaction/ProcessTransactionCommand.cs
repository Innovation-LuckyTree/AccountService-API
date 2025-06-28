using AccountService.Application.Requests.Transactions.Queries;
using MediatR;

namespace AccountService.Application.Requests.Transactions.Commands.ProcessTransaction;

public record ProcessTransactionCommand(long TransactionId) : IRequest<TransactionDto>;
