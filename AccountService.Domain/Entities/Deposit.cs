namespace AccountService.Domain.Entities;

public class Deposit
{
    public long DepositId { get; set; }
    public string Id { get; set; }
    public string AccountId { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public decimal Amount { get; set; }
    public decimal Fee { get; set; }
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
    public string ClientTransactionId { get; set; }
    public string ClientNotes { get; set; }
    public string CallbackUrl { get; set; }
    public string RedirectUrl { get; set; }
    public string ResponseAccountNumber { get; set; }
    public string ResponseAccountQR { get; set; }
    public string ResponseReferenceCode { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset DateRecieved { get; set; } = DateTime.UtcNow;
    public int PaymentProviderId { get; set; }

    public virtual PaymentProvider PaymentProvider { get; set; }

}