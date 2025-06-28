namespace AccountService.Domain.Entities;

public class Withdrawal
{
    public long WithdrawalId { get; set; }
    public string ResponseId { get; set; }
    public Guid UserAccountId { get; set; }
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
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdateAt { get; set; }
    public DateTimeOffset DateRecieved { get; set; } = DateTime.UtcNow;
    public int PaymentProviderId { get; set; }

    public virtual PaymentProvider PaymentProvider { get; set; }
}