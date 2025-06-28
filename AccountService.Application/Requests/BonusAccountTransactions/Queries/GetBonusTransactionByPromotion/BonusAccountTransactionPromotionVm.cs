namespace AccountService.Application.Requests.BonusAccountTransactions.Queries.GetBonusTransactionByPromotion;

public class BonusAccountTransactionPromotionVm
{
    public Guid AccountId { get; set; }
    public long PromotionId { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateExpired { get; set; }
    public decimal ConsumedAmount { get; set; }
    public decimal Balance { get; set; }
    
    public IEnumerable<BonusAccountTransactionDto> AccountTransactions { get; set; }
}