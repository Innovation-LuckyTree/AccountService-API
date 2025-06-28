using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Commands.CreditBonusAccountBalance;

public class CreditBonusAccountBalanceCommand : IRequest<CreditBonusAccountDto>
{
    public Guid AccountId { get; set; }
    public string TransactionNo { get; set; }
    public decimal Amount { get; set; }
    public string Notes { get; set; }
    public string ModeOfTransaction { get; set; }
    public long PromotionId { get; set; }
    public DateTime DateStarted { get; set; }
    public DateTime DateExpired { get; set; }
    public Guid? SourceAccount { get; set; }
}
