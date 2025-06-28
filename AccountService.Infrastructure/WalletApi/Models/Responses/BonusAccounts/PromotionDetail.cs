namespace AccountService.Infrastructure.WalletApi.Models.Responses.BonusAccounts;

public class PromotionDetail
{
    public long PromotionId { get; set; }
    public DateTime DateStarted { get; set; }
    public DateTime ExpirationDate { get; set; }
    public decimal RemainingAmount { get; set; }
    public decimal ConsumedAmount { get; set; }
}
