using MediatR;

namespace AccountService.Application.Requests.Transactions.Queries.GetTransactionNotification;

public record GetTransactionNotificationQuery(long TransactionId) : IRequest<TransactionNotification>;
