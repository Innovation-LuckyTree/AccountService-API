using AccountService.Application.Common.Interfaces;
using AccountService.Common.Interfaces;
using AccountService.Domain.Entities;
using MediatR;

namespace AccountService.Application.Requests.TransactionRequests.Commands.CreateTransaction;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, long>
{
    private readonly IPaymentDataSourceDbContext _paymentDataSourceDbContext;
    private readonly ICurrentUserService _currentUserService;

    public CreateTransactionCommandHandler(IPaymentDataSourceDbContext paymentDataSourceDbContext, ICurrentUserService currentUserService)
    {
        _paymentDataSourceDbContext = paymentDataSourceDbContext;
        _currentUserService = currentUserService;
    }

    public async Task<long> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transactionRequest = new TransactionRequest
        {
            UserAccountId = _currentUserService.UserId,
            Amount = request.Amount,
            TransactionType = request.TransactionType,
            TransactionId = request.TransactionId,
        };

        _paymentDataSourceDbContext.TransactionRequests.Add(transactionRequest);
        await _paymentDataSourceDbContext.SaveChangesAsync(cancellationToken);

        return transactionRequest.TransactionRequestId;
    }
}