using AccountService.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Application.Requests.Transactions.Queries.GetPendingNotification;

public class GetPendingNotificationQueryHandler(IPaymentDataSourceDbContext paymentDataSourceDbContext) : IRequestHandler<GetPendingNotificationQuery, PendingNotificationVm>
{
    private readonly IPaymentDataSourceDbContext _paymentDataSourceDbContext = paymentDataSourceDbContext;

    public async Task<PendingNotificationVm> Handle(GetPendingNotificationQuery request, CancellationToken cancellationToken)
    {
        var unprocessedTransactions = await _paymentDataSourceDbContext.Transactions.Where(o => !o.IsNotified)
            .Select(o => o.TransactionId)
            .ToListAsync(cancellationToken);

        return new PendingNotificationVm(unprocessedTransactions);
    }
}

