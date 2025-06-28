using AccountService.Application.Common.Constants;
using AccountService.Application.Common.Interfaces;
using AccountService.Application.Requests.AccountTransactions.Commands.AddWalletToAccount;
using AccountService.Application.Requests.AccountTransactions.Commands.ProcessWithdrawToAccount;
using AccountService.Application.Requests.Transactions.Commands.UpdateTransaction;
using AccountService.Application.Requests.Transactions.Queries;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.CoreApi.Models.Responses;
using AccountService.Infrastructure.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Application.Requests.Transactions.Commands.ProcessTransaction;

public class ProcessTransactionCommandHandler(IPaymentDataSourceDbContext paymentDataSourceDbContext, IMapper mapper, IMediator mediator, ICoreApiService coreApiService) : IRequestHandler<ProcessTransactionCommand, TransactionDto>
{
    private readonly IPaymentDataSourceDbContext _paymentDataSourceDbContext = paymentDataSourceDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;
    private readonly ICoreApiService _coreApiService = coreApiService;

    public async Task<TransactionDto> Handle(ProcessTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _paymentDataSourceDbContext.Transactions
            .Where(o => o.TransactionId == request.TransactionId)
            .FirstOrDefaultAsync(cancellationToken);

        if (transaction == null)
            return null;

        var transactionDto = _mapper.Map<TransactionDto>(transaction);
        if (transaction.IsProcessed)
            return transactionDto;

        if (transaction.Status.Equals("INITIAL", StringComparison.OrdinalIgnoreCase))
            return await _mediator.Send(new UpdateTransactionCommand(transaction.TransactionId) { Processed = true }, cancellationToken);

        var userAccount = await GetAccountInfo(transaction, cancellationToken);

        if (transaction.Type.Equals("DEPOSIT", StringComparison.CurrentCultureIgnoreCase) && transaction.Status == "SUCCESS")
        {
            await AddWalletRequest(userAccount.AccountObjectId, transaction, cancellationToken);
        }

        if (transaction.Type.Equals("WITHDRAW", StringComparison.CurrentCultureIgnoreCase))
        {
            if (transaction.Status != "SUCCESS" && transaction.TransactionRequestId.HasValue)
            {
                await RefundFailedTransaction(userAccount.AccountObjectId, transaction.TransactionRequestId.Value, cancellationToken);

                return await _mediator.Send(new UpdateTransactionCommand(transaction.TransactionId) { Processed = true }, cancellationToken);
            }

            await WithdrawAccountBalance(userAccount.AccountObjectId, transaction, cancellationToken);
        }

        return await _mediator.Send(new UpdateTransactionCommand(transaction.TransactionId) { Processed = true }, cancellationToken);
    }

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

    private async Task AddWalletRequest(Guid accountId, Transaction transaction, CancellationToken cancellationToken)
    {
        var walletRequest = new AddWalletToAccountCommand
        {
            TransactionNo = transaction.ResponseId,
            AccountId = accountId,
            Amount = transaction.Amount,
            ModeOfTransaction = "GCASH",
            TransactionReference = TransactionReferenceTypes.ACCOUNT_CASH_IN,
            Notes = ""
        };

        await _mediator.Send(walletRequest, cancellationToken);
    }

    private async Task WithdrawAccountBalance(Guid accountId, Transaction transaction, CancellationToken cancellationToken)
    {
        var request = new ProcessWithdrawToAccountCommand
        {
            AccountId = accountId,
            TransactionNo = transaction.ResponseId,
            Amount = transaction.Amount,
            ModeOfTransaction = "GCASH",
            Notes = "Withdraw process",
            Success = transaction.Status == "SUCCESS"
        };

        await _mediator.Send(request, cancellationToken);
    }

    private async Task RefundFailedTransaction(Guid accountId, long transactionRequestId, CancellationToken cancellationToken)
    {
        var transactionReference = await _paymentDataSourceDbContext.TransactionRequests
            .Where(o => o.TransactionRequestId == transactionRequestId)
            .FirstOrDefaultAsync(cancellationToken);

        if (transactionReference == null)
            return;

        var request = new AddWalletToAccountCommand
        {
            AccountId = accountId,
            Amount = transactionReference.Amount,
            ModeOfTransaction = "REFUND",
            TransactionReference = $"{transactionReference.TransactionId}-REFUND",
            Notes = "Failed WITHDRAW Transaction",
            TransactionNo = transactionReference.TransactionId
        };

        await _mediator.Send(request, cancellationToken);
    }
}