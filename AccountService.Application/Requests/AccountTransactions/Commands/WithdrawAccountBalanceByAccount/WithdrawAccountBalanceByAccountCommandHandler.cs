using System.Text.Json;
using AccountService.Application.Common.Constants;
using AccountService.Application.Requests.AccountTransactions.Commands.WithdrawAccountBalance;
using AccountService.Application.Requests.PaymentProviders.Commands.RbgiWithdrawAccountBalance;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.Requests.AccountTransactions.Commands.WithdrawAccountBalanceByAccount;

public class WithdrawAccountBalanceByAccountCommandHandler(ICoreApiService coreApiService, IWalletApiService walletApiService, IMediator mediator, ILogger<WithdrawAccountBalanceByAccountCommandHandler> logger) : IRequestHandler<WithdrawAccountBalanceByAccountCommand, WithdrawAccountDto>
{
    private readonly ICoreApiService _coreApiService = coreApiService;
    private readonly IWalletApiService _walletApiService = walletApiService;
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<WithdrawAccountBalanceByAccountCommandHandler> _logger = logger;

    public async Task<WithdrawAccountDto> Handle(WithdrawAccountBalanceByAccountCommand request, CancellationToken cancellationToken)
    {
        var accountInfo = await _coreApiService.GetAccountByAccountObjectId(request.AccountId, cancellationToken);

        var currentWalletBalance = await _walletApiService.GetAccountWalletBalance(accountInfo.AccountObjectId, cancellationToken);

        if (request.Amount > currentWalletBalance.Balance)
        {
            _logger.LogWarning($"Player attempted to withdraw amount {request.Amount} with insuficient balance: ${accountInfo.FullName}. current wallet balance {currentWalletBalance.Balance}");
            throw new Exception("Account Insuficient Balance!");
        }
        var amount = request.Amount > 0 ? request.Amount * -1 : request.Amount;

        var sendWithdrawRequest = new RbgiWithdrawAccountBalanceCommand(accountInfo.AccountObjectId, accountInfo.PaymentAccount, request.Amount, accountInfo.FullName, accountInfo.MobileNumber, request.TransactionNo);
        var withdrawResponse = await _mediator.Send(sendWithdrawRequest, cancellationToken);

        if (withdrawResponse != null || withdrawResponse.Status.Equals("Status", StringComparison.CurrentCultureIgnoreCase))
        {
            var withdrawResponseJson = JsonSerializer.Serialize(withdrawResponse);
            _logger.LogInformation($"Amount successfully credited to the account! Withdraw response: {withdrawResponseJson}");

            _logger.LogInformation($"Processing credit account to main wallet");
            var creditRequest = new AddCreditTransactionRequest(accountInfo.AccountObjectId,
                AccountTypes.ACCOUNT_PLAYER, request.TransactionNo, TransactionReferenceTypes.ACCOUNT_WITHDRAW,
                amount, request.ModeOfTransaction, request.Notes);

            await _walletApiService.AddCreditTransactionRequest(creditRequest, cancellationToken);

            var result = await _walletApiService.GetAccountWalletBalance(accountInfo.AccountObjectId, cancellationToken);
            return new WithdrawAccountDto(result);
        }


        return new WithdrawAccountDto()
        {
            Status = "failed",
            ErrorMessage = "Unable to connect the payment provider."
        };
    }
}