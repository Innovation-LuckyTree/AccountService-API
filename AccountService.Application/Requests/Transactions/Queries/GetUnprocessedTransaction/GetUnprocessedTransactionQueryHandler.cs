using AccountService.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Application.Requests.Transactions.Queries.GetUnprocessedTransaction;

public class GetUnprocessedTransactionQueryHandler(IPaymentDataSourceDbContext paymentDataSourceDbContext) : IRequestHandler<GetUnprocessedTransactionQuery, UnprocessedTransactionVm>
{
    private readonly IPaymentDataSourceDbContext _paymentDataSourceDbContext = paymentDataSourceDbContext;

    public async Task<UnprocessedTransactionVm> Handle(GetUnprocessedTransactionQuery request, CancellationToken cancellationToken)
    {
        var unprocessedTransactions = await _paymentDataSourceDbContext.Transactions.Where(o => !o.IsProcessed)
            .Select(o => o.TransactionId)
            .ToListAsync(cancellationToken);

        return new UnprocessedTransactionVm(unprocessedTransactions);
    }
}



