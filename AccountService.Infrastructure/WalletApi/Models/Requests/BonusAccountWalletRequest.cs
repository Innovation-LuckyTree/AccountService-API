namespace AccountService.Infrastructure.WalletApi.Models.Requests
{
    public record WalletRequest
    {
        //adding real wallet request needed
        public string WalletId { get; set; }
        public string TransactionNo { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public decimal Amount { get; set; }
    }
}
