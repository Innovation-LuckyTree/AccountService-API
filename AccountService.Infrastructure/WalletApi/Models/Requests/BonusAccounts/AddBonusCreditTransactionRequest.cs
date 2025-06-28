namespace AccountService.Infrastructure.WalletApi.Models.Requests.BonusAccounts;

public record AddBonusCreditTransactionRequest(Guid AccountId, string AccountType, string TransactionNo, string TransactionReference, decimal Amount, string ModeOfTransaction, string? Notes)
{
    public long PromotionId { get; set; }
    public DateTime DateStarted { get; set; }
    public DateTime DateExpired { get; set; }
    public Guid? SourceAccount { get; set; }
}
