using System.Runtime.CompilerServices;
using AccountService.Application.Common.Constants;
using AccountService.Application.Common.Enums;
using AccountService.Infrastructure.CoreApi.Models.Requests;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests;
using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Commands.AddWalletToAccount;

public class AddWalletToAccountCommandHandler : IRequestHandler<AddWalletToAccountCommand, AccountBalanceResponse>
{
    private readonly IWalletApiService _walletApiService;
    private readonly ICoreApiService _coreApiService;


    public AddWalletToAccountCommandHandler(IWalletApiService walletApiService, ICoreApiService coreApiService)
    {
        _walletApiService = walletApiService;
        _coreApiService = coreApiService;

    }

    public async Task<AccountBalanceResponse> Handle(AddWalletToAccountCommand request, CancellationToken cancellationToken)
    {
        var accountType = !string.IsNullOrEmpty(request.AccountType) ? request.AccountType : AccountTypes.ACCOUNT_PLAYER;

        var debitRequest = new AddDebitTransactionRequest(request.AccountId,
            accountType, request.TransactionNo, request.TransactionReference,
            request.ModeOfTransaction, request.Amount, request.Notes);

        await _walletApiService.AddDebitTransactionRequest(debitRequest, cancellationToken);

        var depositRequest = new UserDepositTransactionRequest(debitRequest.Amount, (int)PaymentMethodTypes.GCash, "App", "");

        await _coreApiService.SaveUserDepositTransaction(depositRequest, cancellationToken);

        return await _walletApiService.GetAccountWalletBalance(request.AccountId, cancellationToken);
    }
}
