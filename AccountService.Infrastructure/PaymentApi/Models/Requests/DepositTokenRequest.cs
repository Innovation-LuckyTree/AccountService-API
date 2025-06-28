namespace AccountService.Infrastructure.Clients.ConnectPay.Models.Requests;

public class DepositTokenRequest
{
    public string MerchantName { get; set; }
    public string AccountId { get; set; }
    public decimal Amount { get; set; }
    public string AccountName { get; set; }
    public string ClientTransactionId { get; set; }
    public string? ClientNotes { get; set; }
    public string? CallbackUrl { get; set; }
    public string? RedirectUrl { get; set; } = "";
}