using AccountService.Application.Common.Constants;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests.BonusAccounts;
using AccountService.Infrastructure.WalletApi.Models.Responses.BonusAccounts;
using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Commands.AddBetByBonus;

public class AddBetByBonusCommandHandler(IWalletApiService walletApiService) : IRequestHandler<AddBetByBonusCommand, BonusAccountBalanceResponse>
{
    private readonly IWalletApiService _walletApiService = walletApiService;

    public async Task<BonusAccountBalanceResponse> Handle(AddBetByBonusCommand request, CancellationToken cancellationToken)
    {
        var currentWalletBalance = await _walletApiService.GetBonusAccountBalance(request.AccountId, cancellationToken);

        if (request.Amount > currentWalletBalance.Balance)
        {
            throw new Exception("Bonus Account Insuficient Balance!");
        }
        var amount = request.Amount > 0 ? request.Amount * -1 : request.Amount;

        var creditRequest = new AddBonusCreditTransactionRequest(request.AccountId, 
            AccountTypes.ACCOUNT_PLAYER, request.TransactionNo, TransactionReferenceTypes.ACCOUNT_BET,
            amount, "", request.Notes)
            {
                PromotionId = request.PromotionId,
                DateStarted = request.DateStarted,
                DateExpired = request.DateExpired
            };

        await _walletApiService.AddBonusCreditTransactionRequest(creditRequest, cancellationToken);

        return await _walletApiService.GetBonusAccountBalance(request.AccountId, cancellationToken);
    }
}