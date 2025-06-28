namespace AccountService.Infrastructure.PaymentApi.Models.Responses;

public class DepositTokenResponse : ApiResponse<DepositTokenData>
{
}

public class DepositTokenData
{
    public string Base { get; set; }
    public string Token { get; set; }
    public string Url { get; set; }
    public string Status { get; set; }
    public decimal Amount { get; set; }
    public decimal Fee { get; set; }
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
    public string ClientTransactionId { get; set; }
    public string ClientNotes { get; set; }
    public string CallbackUrl { get; set; }
    public string RedirectUrl { get; set; }
    public DepositReceiverResponse Receiver { get; set; }
}

