using AccountService.Application.Common.Constants;
using AccountService.Application.Requests.PaymentProviders.Commands.WithdrawAccount;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Commands.WithdrawAccountBalance;

public class WithdrawAccountBalanceCommandHandler : IRequestHandler<WithdrawAccountBalanceCommand, WithdrawAccountDto>
{
    private readonly ICoreApiService _coreApiService;
    private readonly IWalletApiService _walletApiService;
    private readonly IMediator _mediator;

        public WithdrawAccountBalanceCommandHandler(ICoreApiService coreApiService, IWalletApiService walletApiService, IMediator mediator)
        {
            _coreApiService = coreApiService;
            _walletApiService = walletApiService;
            _mediator = mediator;

        }

    public async Task<WithdrawAccountDto> Handle(WithdrawAccountBalanceCommand request, CancellationToken cancellationToken)
    {
        var accountInfo = await _coreApiService.GetAccountByAccountObjectId(request.AccountId, cancellationToken);
        var currentWalletBalance = await _walletApiService.GetAccountWalletBalance(request.AccountId, cancellationToken);

        if (request.Amount >= currentWalletBalance.Balance)
        {
            return new WithdrawAccountDto
            {
                Status = "failed",
                ErrorMessage = "Account Insufficient Balance!"
            };
        }
        var amount = request.Amount > 0 ? request.Amount * -1 : request.Amount;

        var sendWithdrawRequest = new WithdrawAccountCommand(accountInfo.PaymentAccount, request.Amount, accountInfo.FullName, accountInfo.MobileNumber, request.TransactionNo);
        var withdrawResponse = await _mediator.Send(sendWithdrawRequest, cancellationToken);

        if (withdrawResponse != null && withdrawResponse.Data != null && withdrawResponse.Data.Ok)
        {
            var creditRequest = new AddCreditTransactionRequest(request.AccountId,
                AccountTypes.ACCOUNT_PLAYER, request.TransactionNo, TransactionReferenceTypes.ACCOUNT_WITHDRAW,
                amount, request.ModeOfTransaction, request.Notes);

            await _walletApiService.AddCreditTransactionRequest(creditRequest, cancellationToken);

            var result = await _walletApiService.GetAccountWalletBalance(request.AccountId, cancellationToken);
            return new WithdrawAccountDto(result);
        }

        return new WithdrawAccountDto()
        {
            Status = "failed",
            ErrorMessage = "Unable to connect the payment provider."
        };
    }
}