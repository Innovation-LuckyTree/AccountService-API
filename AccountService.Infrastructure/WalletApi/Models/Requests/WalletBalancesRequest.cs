namespace AccountService.Infrastructure.WalletApi.Models.Requests
{
    public class WalletBalancesRequest
    {
        public IEnumerable<Guid> AccountIds { get; set; }
    }
}
