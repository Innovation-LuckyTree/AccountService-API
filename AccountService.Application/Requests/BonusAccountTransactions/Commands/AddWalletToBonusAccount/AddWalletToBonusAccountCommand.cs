using AccountService.Infrastructure.WalletApi.Models.Responses.BonusAccounts;
using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Commands.AddWalletToBonusAccount;

public class AddWalletToBonusAccountCommand : IRequest<BonusAccountBalanceResponse>
{
    public Guid AccountId { get; set; }
    public string TransactionNo { get; set; }
    public string TransactionReference { get; set; }
    public decimal Amount { get; set; }
    public string ModeOfTransaction { get; set; }
    public string Notes { get; set; }
    public string? AccountType { get; set; }
    public long PromotionId { get; set; }
    public DateTime DateStarted { get; set; }
    public DateTime DateExpired { get; set; }
    public Guid? SourceAccount { get; set; }
}
