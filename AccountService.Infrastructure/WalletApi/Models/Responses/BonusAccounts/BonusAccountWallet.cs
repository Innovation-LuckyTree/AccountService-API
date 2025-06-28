namespace AccountService.Infrastructure.WalletApi.Models.Responses.BonusAccounts;

public class Wallet
{
    public string WalletId { get; set; }
    public string TransactionNo { get; set; }
    public DateTime DateOfTransaction { get; set; }
    public decimal Amount { get; set; }
}
