using AccountService.Application.Requests.Transactions.Queries;
using MediatR;

namespace AccountService.Application.Requests.Transactions.Commands.UpdateTransaction;

public record UpdateTransactionCommand(long TransactionId) : IRequest<TransactionDto>
{
    public bool Processed { get; set; } = false;
    public bool Notified { get; set; } = false;
}
