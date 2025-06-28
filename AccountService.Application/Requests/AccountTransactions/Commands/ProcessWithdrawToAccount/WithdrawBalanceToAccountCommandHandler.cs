using AccountService.Application.Common.Constants;
using AccountService.Application.Requests.AccountTransactions.Commands.AddWalletToAccount;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests;
using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Commands.ProcessWithdrawToAccount;

public class ProcessWithdrawToAccountCommandHandler : IRequestHandler<ProcessWithdrawToAccountCommand, AccountBalanceResponse>
{

    private readonly ICoreApiService _coreApiService;
    private readonly IWalletApiService _walletApiService;
    private readonly IMediator _mediator;

    public ProcessWithdrawToAccountCommandHandler(ICoreApiService coreApiService, IWalletApiService walletApiService, IMediator mediator)
    {
        _coreApiService = coreApiService;
        _walletApiService = walletApiService;
        _mediator = mediator;

    }

    public async Task<AccountBalanceResponse> Handle(ProcessWithdrawToAccountCommand request, CancellationToken cancellationToken)
    {
        if (!request.Success)
        {
            await AddWalletRequest(request, cancellationToken);
        }

        return await _walletApiService.GetAccountWalletBalance(request.AccountId, cancellationToken);
    }

    private async Task AddWalletRequest(ProcessWithdrawToAccountCommand request, CancellationToken cancellationToken)
    {
        var walletRequest = new AddWalletToAccountCommand
        {
            TransactionNo = request.TransactionNo,
            AccountId = request.AccountId,
            Amount = request.Amount,
            ModeOfTransaction = "REFUND",
            TransactionReference = TransactionReferenceTypes.ACCOUNT_CASH_IN,
            Notes = "FAILED GCASH TRANSACTION"
        };

        await _mediator.Send(walletRequest, cancellationToken);
    }
}