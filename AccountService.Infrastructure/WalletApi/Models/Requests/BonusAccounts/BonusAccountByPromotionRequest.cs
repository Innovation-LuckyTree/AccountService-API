namespace AccountService.Infrastructure.WalletApi.Models.Requests.BonusAccounts;

public record BonusAccountByPromotionRequest(Guid AccountId, long PromotionId, DateTime DateStart, DateTime DateExpired)
{
}
