namespace AccountService.Infrastructure.WalletApi.Models.Requests.BonusAccounts;

public record BonusWalletRequest
{
    //adding real wallet request needed
    public string WalletId { get; set; }
    public string TransactionNo { get; set; }
    public DateTime DateOfTransaction { get; set; }
    public decimal Amount { get; set; }
}
