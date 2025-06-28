using AccountService.Application.Common.Constants;
using AccountService.Application.Requests.BonusAccountTransactions.Commands.CreditBonusAccountBalance;
using AccountService.Application.Requests.BonusAccountTransactions.Queries.GetBonusTransactionByPromotion;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests.BonusAccounts;
using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Commands.ProcessReturnBonus;

public class ProcessReturnBonusCommandHandler(IWalletApiService walletApiService, IMediator mediator) : IRequestHandler<ProcessReturnBonusCommand, CreditBonusAccountDto>
{
    private readonly IWalletApiService _walletApiService = walletApiService;
    private readonly IMediator _mediator = mediator;

    public async Task<CreditBonusAccountDto> Handle(ProcessReturnBonusCommand request, CancellationToken cancellationToken)
    {
        var accountBalanceRequest = new BonusAccountByPromotionRequest(request.AccountId, request.PromotionId, request.DateStart, request.DateExpired);

        var bonusAccountBalance = await _mediator.Send(new GetBonusTransactionByPromotionQuery(accountBalanceRequest), cancellationToken);

        if ((bonusAccountBalance?.Balance ?? 0) == 0)
        {
            // TODO: need to return the current balance
            return null;
        }

        var amount = (request.Amount ?? 0) > 0 ? request.Amount.Value : bonusAccountBalance.Balance;

        if (request.Amount > bonusAccountBalance.Balance)
        {
            amount = bonusAccountBalance.Balance;
        }

        if (request.IsExpire)
        {
            amount = bonusAccountBalance.Balance;
        }

        amount = amount > 0 ? amount * -1 : amount;

        var notes = request.IsExpire ? "Expired Bonus" : "Return Bonus Amount";

        var creditRequest = new AddBonusCreditTransactionRequest(request.AccountId,
            AccountTypes.ACCOUNT_PLAYER, request.TransactionNo, TransactionReferenceTypes.BONUS_ACCOUNT_CREDIT,
            amount, "Automated", notes)
        {
            PromotionId = request.PromotionId, 
            DateStarted = request.DateStart,
            DateExpired = request.DateExpired
        };

        await _walletApiService.AddBonusCreditTransactionRequest(creditRequest, cancellationToken);

        var result = await _walletApiService.GetBonusAccountBalance(request.AccountId, cancellationToken);
        return new CreditBonusAccountDto(result);
    }
}