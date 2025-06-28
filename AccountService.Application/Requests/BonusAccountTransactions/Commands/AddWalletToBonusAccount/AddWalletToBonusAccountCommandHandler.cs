using AccountService.Application.Common.Constants;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests.BonusAccounts;
using AccountService.Infrastructure.WalletApi.Models.Responses.BonusAccounts;
using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Commands.AddWalletToBonusAccount;

public class AddWalletToBonusAccountCommandHandler(IWalletApiService walletApiService) : IRequestHandler<AddWalletToBonusAccountCommand, BonusAccountBalanceResponse>
{
    private readonly IWalletApiService _walletApiService = walletApiService;

    public async Task<BonusAccountBalanceResponse> Handle(AddWalletToBonusAccountCommand request, CancellationToken cancellationToken)
    {
        var accountType = !string.IsNullOrEmpty(request.AccountType) ? request.AccountType : AccountTypes.ACCOUNT_BONUS;

        var debitRequest = new AddBonusDebitTransactionRequest(request.AccountId,
            accountType, request.TransactionNo, request.TransactionReference,
            request.ModeOfTransaction, request.Amount, request.Notes)
        {
            PromotionId = request.PromotionId,
            DateStarted = request.DateStarted,
            DateExpired = request.DateExpired,
            SourceAccount = request.SourceAccount
        };

        await _walletApiService.AddBonusDebitTransactionRequest(debitRequest, cancellationToken);

        // var depositRequest = new UserDepositTransactionRequest(debitRequest.Amount, (int)PaymentMethodTypes.GCash, "App", "");

        // await _coreApiService.SaveUserDepositTransaction(depositRequest, cancellationToken);

        return await _walletApiService.GetBonusAccountBalance(request.AccountId, cancellationToken);
    }
}
