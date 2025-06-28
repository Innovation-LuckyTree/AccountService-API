using AccountService.Application.Common.Constants;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests.BonusAccounts;
using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Commands.CreditBonusAccountBalance;

public class CreditBonusAccountBalanceCommandHandler(IWalletApiService walletApiService) : IRequestHandler<CreditBonusAccountBalanceCommand, CreditBonusAccountDto>
{
    private readonly IWalletApiService _walletApiService = walletApiService;

    public async Task<CreditBonusAccountDto> Handle(CreditBonusAccountBalanceCommand request, CancellationToken cancellationToken)
    {
        var currentWalletBalance = await _walletApiService.GetBonusAccountBalance(request.AccountId, cancellationToken);

        if (request.Amount > currentWalletBalance.Balance)
        {
            throw new Exception("Account Insuficient Balance!");
        }
        var amount = request.Amount > 0 ? request.Amount * -1 : request.Amount;

        var creditRequest = new AddBonusCreditTransactionRequest(request.AccountId,
            AccountTypes.ACCOUNT_PLAYER, request.TransactionNo, TransactionReferenceTypes.BONUS_ACCOUNT_CREDIT,
            amount, request.ModeOfTransaction, request.Notes)
        {
            PromotionId = request.PromotionId,
            DateStarted = request.DateStarted,
            DateExpired = request.DateExpired,
            SourceAccount = request.SourceAccount
        };

        await _walletApiService.AddBonusCreditTransactionRequest(creditRequest, cancellationToken);

        var result = await _walletApiService.GetBonusAccountBalance(request.AccountId, cancellationToken);
        return new CreditBonusAccountDto(result);
    }
}