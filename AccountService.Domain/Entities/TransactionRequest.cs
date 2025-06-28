namespace AccountService.Domain.Entities;

public class TransactionRequest
{
    public long TransactionRequestId { get; set; }
    public string UserAccountId { get; set; }
    public string TransactionType { get; set; }
    public string TransactionId { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset CreatedOn { get; set; } = DateTime.UtcNow;
}