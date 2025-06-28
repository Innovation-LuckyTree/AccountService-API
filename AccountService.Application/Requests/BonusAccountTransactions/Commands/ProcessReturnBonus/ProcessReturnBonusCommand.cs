using AccountService.Application.Requests.BonusAccountTransactions.Commands.CreditBonusAccountBalance;
using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Commands.ProcessReturnBonus;

public class ProcessReturnBonusCommand : IRequest<CreditBonusAccountDto>
{
    public Guid AccountId { get; set; }
    public long PromotionId { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateExpired { get; set; }
    public bool IsExpire { get; set; }
    public string TransactionNo { get; set; }
    public decimal? Amount { get; set; }
}
