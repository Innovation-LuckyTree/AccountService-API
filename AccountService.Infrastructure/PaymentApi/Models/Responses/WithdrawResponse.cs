namespace AccountService.Infrastructure.PaymentApi.Models.Responses;

public class WithdrawResponse : ApiResponse<WithdrawData>
{

}

public class WithdrawData
{
    public string Id { get; set; }
    public string AccountId { get; set; }

    public string Type { get; set; }
    public string Status { get; set; }
    public decimal Amount { get; set; }
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
    public string ClientTransactionId { get; set; }
    public string ClientNotes { get; set; }
    public string CallbackUrl { get; set; }
    public string RedirectUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
}