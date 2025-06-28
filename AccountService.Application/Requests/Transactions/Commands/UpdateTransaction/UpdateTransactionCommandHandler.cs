
using AccountService.Application.Common.Interfaces;
using AccountService.Application.Requests.Transactions.Queries;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Application.Requests.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler(IPaymentDataSourceDbContext paymentDataSourceDbContext, IMapper mapper) : IRequestHandler<UpdateTransactionCommand, TransactionDto>
{
    private readonly IPaymentDataSourceDbContext _paymentDataSourceDbContext = paymentDataSourceDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<TransactionDto> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _paymentDataSourceDbContext.Transactions
            .Where(o => o.TransactionId == request.TransactionId)
            .FirstOrDefaultAsync(cancellationToken);

        if (transaction == null)
            return null;

        if (request.Processed)
        {
            transaction.IsProcessed = true;
            transaction.ProcessDate = DateTime.Now;
        }

        if (request.Notified)
        {
            transaction.IsNotified = true;
            transaction.NotifiedDate = DateTime.Now;
        }

        await _paymentDataSourceDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TransactionDto>(transaction);
    }
}