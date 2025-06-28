using MediatR;

namespace AccountService.Application.Requests.TransactionRequests.Commands.CreateTransaction;

public record CreateTransactionCommand(decimal Amount, string? TransactionType, string? TransactionId) : IRequest<long>;
