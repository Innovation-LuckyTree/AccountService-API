using AccountService.Infrastructure.WalletApi.Models.Responses.BonusAccounts;
using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Commands.AddBetByBonus;

public class AddBetByBonusCommand : IRequest<BonusAccountBalanceResponse>
{
    public Guid AccountId { get; set; }
    public string TransactionNo { get; set; }
    public decimal Amount { get; set; }
    public string Notes { get; set; }
    public long PromotionId { get; set; }
    public DateTime DateStarted { get; set; }
    public DateTime DateExpired { get; set; }
}
