namespace AccountService.Infrastructure.WalletApi.Models.Responses
{
    public record WalletBalancesResponse(IEnumerable<AccountListBalanceDto> AccountBalances)
    {
        public int Count { get; } = AccountBalances?.Count() ?? 0;
    }

    public class AccountListBalanceDto
    {
        public Guid AccountId { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
