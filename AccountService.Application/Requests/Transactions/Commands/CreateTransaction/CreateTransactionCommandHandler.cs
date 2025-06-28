using AccountService.Application.Common.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.CoreApi.Models.Responses;
using AccountService.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Application.Requests.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler(IPaymentDataSourceDbContext paymentDataSourceDbContext, ICoreApiService coreApiService) : IRequestHandler<CreateTransactionCommand, CreateTransactionDto>
{
    private readonly IPaymentDataSourceDbContext _paymentDataSourceDbContext = paymentDataSourceDbContext;
    private readonly ICoreApiService _coreApiService = coreApiService;

    public async Task<CreateTransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = CreateTransaction(request);
        var userAccount = await GetAccountInfo(request, cancellationToken);

        if (userAccount != null)
            transaction.UserAccountId = userAccount.UserId;

        _paymentDataSourceDbContext.Transactions.Add(transaction);

        await _paymentDataSourceDbContext.SaveChangesAsync(cancellationToken);

        return new CreateTransactionDto(transaction.TransactionObjectId);
    }

    private async Task<AccountInfo> GetAccountInfo(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        // if the request has transactionRequest Id, then get the account based on the user account stored in db 
        if ((request.CoreTransactionId ?? 0) > 0)
        {
            var accountInfo = await GetAccountInfoByTransaction(request.CoreTransactionId.Value, cancellationToken);

            if (accountInfo != null)
            {
                return accountInfo;
            }
        }

        // if the transactionRequestId is empty, then get the account based on the user paymentAccount Id
        var accountByPaymentAccountId = await _coreApiService.GetAccountInfoByPaymentAccount(request.AccountId, cancellationToken);
        if (accountByPaymentAccountId != null)
        {
            return accountByPaymentAccountId;
        }

        // last way to get the account based on the mobile number
        var accountByMobileNumber = await _coreApiService.GetUserByMobile(request.AccountNumber, cancellationToken);
        if (accountByMobileNumber != null)
        {
            return accountByMobileNumber;
        }

        return null;
    }

    private async Task<AccountInfo> GetAccountInfoByTransaction(long transactionRequestId, CancellationToken cancellationToken)
    {
        var transactionRequest = await _paymentDataSourceDbContext.TransactionRequests
             .Where(o => o.TransactionRequestId == transactionRequestId)
             .FirstOrDefaultAsync(cancellationToken);

        if (transactionRequest == null)
        {
            return null;
        }

        return await _coreApiService.GetAccountInfoByUserId(Guid.Parse(transactionRequest.UserAccountId), cancellationToken);
    }

    private Transaction CreateTransaction(CreateTransactionCommand request) =>
        new Transaction
        {
            ResponseId = request.Id,
            AccountId = request.AccountId,
            Type = request.Type,
            Status = request.Status,
            StatusNotes = request.StatusNotes,
            Amount = request.Amount,
            AccountName = request.AccountName,
            AccountNumber = request.AccountNumber,
            ClientTransactionId = request.ClientTransactionId,
            ClientNotes = request.ClientNotes,
            CallbackUrl = request.CallbackUrl,
            RedirectUrl = request.RedirectUrl,
            CreatedAt = request.CreatedAt,
            UpdatedAt = request.UpdatedAt,
            PaymentProviderId = request.PaymentProviderId,
            TransactionRequestId = request.CoreTransactionId
        };
}