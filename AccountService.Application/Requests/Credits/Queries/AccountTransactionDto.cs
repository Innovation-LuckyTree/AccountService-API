namespace AccountService.Application.Requests.Credits.Queries;

public class AccountTransactionDto
{
    public Guid Id { get; set; }
    public string TransactionNo { get; set; }
    public int TransactionType { get; set; }
    public string TransactionReference { get; set; }
    public decimal Amount { get; set; }
    public decimal Credit { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Notes { get; set; }
}