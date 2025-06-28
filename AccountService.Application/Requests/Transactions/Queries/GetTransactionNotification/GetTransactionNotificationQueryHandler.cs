using AccountService.Application.Common.Constants;
using AccountService.Application.Common.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.CoreApi.Models.Responses;
using AccountService.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Application.Requests.Transactions.Queries.GetTransactionNotification;

public class GetTransactionNotificationQueryHandler(IPaymentDataSourceDbContext paymentDataSourceDbContext, ICoreApiService coreApiService) : IRequestHandler<GetTransactionNotificationQuery, TransactionNotification>
{
    private readonly IPaymentDataSourceDbContext _paymentDataSourceDbContext = paymentDataSourceDbContext;
    private readonly ICoreApiService _coreApiService = coreApiService;

    public async Task<TransactionNotification> Handle(GetTransactionNotificationQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _paymentDataSourceDbContext.Transactions
            .Where(o => o.TransactionId == request.TransactionId)
            .FirstOrDefaultAsync(cancellationToken);

        if (transaction == null)
            return null;

        var accountInfo = await GetAccountInfo(transaction, cancellationToken);

        if (accountInfo == null)
            return null;

        return GetTransactionNotification(transaction, accountInfo);
    }

    private TransactionNotification GetTransactionNotification(Transaction transaction, AccountInfo accountInfo)
    {
        return new TransactionNotification
        {
            Type = "Wallet",
            NotificationName = transaction.Type switch
            {
                "DEPOSIT" => ProcessDepositRequest(transaction.Status),
                "WITHDRAW" => ProcessWithdrawRequest(transaction.Status)
            },
            Args = [transaction.Amount.ToString("F2")],
            Accounts = [ new NotificationAccount
            {
                Name = accountInfo.FullName,
                UserId = accountInfo.UserId,
                AccountId = accountInfo.AccountInfoId,
                UserTypeId = 5
            }]
        };
    }

    private string ProcessDepositRequest(string status)
        => status switch
        {
            "FAILED" => GCashNotificationNames.DECLINED_GCASH_DEPOSIT,
            "SUCCESS" => GCashNotificationNames.APPROVED_GCASH_DEPOSIT,
            _ => GCashNotificationNames.PROCESSED_GCASH_DEPOSIT
        };

    private string ProcessWithdrawRequest(string status)
        => status switch
        {
            "FAILED" => GCashNotificationNames.DECLINED_GCASH_WITHDAWAL,
            "SUCCESS" => GCashNotificationNames.APPROVED_GCASH_WITHDRAWAL,
            _ => GCashNotificationNames.PROCESSED_GCASH_WITHDRAWAL
        };


    private async Task<AccountInfo> GetAccountInfo(Transaction transaction, CancellationToken cancellationToken)
    {
        // if the request has transactionRequest Id, then get the account based on the user account stored in db 
        if ((transaction.TransactionRequestId ?? 0) > 0)
        {
            var accountInfo = await GetAccountInfoByTransaction(transaction.TransactionRequestId.Value, cancellationToken);

            if (accountInfo != null)
            {
                return accountInfo;
            }
        }

        // if the transactionRequestId is empty, then get the account based on the user paymentAccount Id
        var accountByPaymentAccountId = await _coreApiService.GetAccountInfoByPaymentAccount(transaction.AccountId, cancellationToken);
        if (accountByPaymentAccountId != null)
        {
            return accountByPaymentAccountId;
        }

        // last way to get the account based on the mobile number
        var accountByMobileNumber = await _coreApiService.GetUserByMobile(transaction.AccountNumber, cancellationToken);
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
}