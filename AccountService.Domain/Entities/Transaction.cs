namespace AccountService.Domain.Entities;

public class Transaction
{
    public long TransactionId { get; set; }
    public Guid TransactionObjectId { get; set; } = Guid.NewGuid();
    public string ResponseId { get; set; }
    public Guid UserAccountId { get; set; }
    public string AccountId { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public string StatusNotes { get; set; }
    public decimal Amount { get; set; }
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
    public string ClientTransactionId { get; set; }
    public string ClientNotes { get; set; }
    public string CallbackUrl { get; set; }
    public string RedirectUrl { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset DateRecieved { get; set; } = DateTime.UtcNow;
    public int PaymentProviderId { get; set; }
    public Guid CompanyId { get; set; }
    public bool IsProcessed { get; set; } = false;
    public bool IsNotified { get; set; } = false;
    public DateTimeOffset? ProcessDate { get; set; } = null;
    public DateTimeOffset? NotifiedDate { get; set; } = null;
    public long? TransactionRequestId { get; set; }

    public virtual PaymentProvider PaymentProvider { get; set; }
}