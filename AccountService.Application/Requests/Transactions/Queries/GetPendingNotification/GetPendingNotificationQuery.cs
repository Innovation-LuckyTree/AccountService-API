using MediatR;

namespace AccountService.Application.Requests.Transactions.Queries.GetPendingNotification;

public class GetPendingNotificationQuery : IRequest<PendingNotificationVm>;

