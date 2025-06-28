namespace AccountService.Infrastructure.WalletApi.Models.Requests.BonusAccounts;

public class PagedBonusTransactionRequest
{
    public Guid AccountId { get; set; }
    public string SearchKey { get; set; }
    public int? TransactionType { get; set; }
    public long? PromotionId { get; set; }
    public DateTime? PromotionStarted { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public int Start { get; set; } = 0;
    public int PageSize { get; set; } = 20;
    public DateTime? StartDate { get; set; } = DateTime.Now.AddDays(-7);
    public DateTime? EndDate { get; set; } = DateTime.Now;
}
