namespace AccountService.Application.Requests.Credits.Queries;

public class AccountDto
{
    public Guid AccountId { get; set; }
    public string AccountType { get; set; }

    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
    public decimal Balance { get; set; }

    public int Offset { get; set; }
    public int TotalCount { get; set; }
    public int DebitTransactionCount { get; set; }
    public int CreditsTransactionCount { get; set; }
    public int TransactionCount { get; set; }
    
    public IEnumerable<AccountTransactionDto> Transactions { get; set; }
}