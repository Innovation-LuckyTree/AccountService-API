namespace AccountService.Application.Requests.BonusAccountTransactions.Queries;

public class BonusAccountDto
{
    public Guid AccountId { get; set; }
    public string AccountType { get; set; }
    public int Offset { get; set; }
    public int TotalCount { get; set; }

    public decimal TotalDebit { get; private set; }
    public decimal TotalCredit { get; private set; }

    public int DebitTransactionCount { get; private set; }

    public int CreditsTransactionCount { get; private set; }

    public int TransactionCount { get; set; }

    public decimal Balance { get; set;}

    public IEnumerable<BonusAccountTransactionDto> BonusWalletTransactions { get; set; }
}