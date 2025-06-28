namespace AccountService.Infrastructure.WalletApi.Models.Responses.BonusAccounts;

public class BonusAccountTransaction
{
    public Guid AccountId { get; set; }
    public string AccountType { get; set; }
    public decimal Balance { get; set; }
    public IEnumerable<PromotionDetail> PromotionDetails { get; set; }
}