using AccountService.Application.Common.Constants;
using AccountService.Application.Requests.AccountTransactions.Commands.WithdrawAccountBalance;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Commands.ProcessWithdrawAccount;

public class ProcessWithdrawAccountCommandHandler : IRequestHandler<ProcessWithdrawAccountCommand, WithdrawAccountDto>
{
    private readonly ICoreApiService _coreApiService;
    private readonly IWalletApiService _walletApiService;

    public ProcessWithdrawAccountCommandHandler(ICoreApiService coreApiService, IWalletApiService walletApiService)
    {
        _coreApiService = coreApiService;
        _walletApiService = walletApiService;
    }

    public async Task<WithdrawAccountDto> Handle(ProcessWithdrawAccountCommand request, CancellationToken cancellationToken)
    {
        var accountInfo = await _coreApiService.GetAccountByAccountObjectId(request.AccountId, cancellationToken);

        var currentWalletBalance = await _walletApiService.GetAccountWalletBalance(accountInfo.AccountObjectId, cancellationToken);

        if (request.Amount > currentWalletBalance.Balance)
        {
            throw new Exception("Account Insuficient Balance!");
        }
        var amount = request.Amount > 0 ? request.Amount * -1 : request.Amount;

        var creditRequest = new AddCreditTransactionRequest(accountInfo.AccountObjectId,
            AccountTypes.ACCOUNT_PLAYER, request.TransactionNo, TransactionReferenceTypes.ACCOUNT_WITHDRAW,
            amount, request.ModeOfTransaction, request.Notes);

        await _walletApiService.AddCreditTransactionRequest(creditRequest, cancellationToken);

        var result = await _walletApiService.GetAccountWalletBalance(accountInfo.AccountObjectId, cancellationToken);
        return new WithdrawAccountDto(result);
    }
}