namespace AccountService.Application.Requests.Transactions.Queries;

public record TransactionVm(IEnumerable<TransactionDto> Transactions)
{
    public int Count
    {
        get
        {
            return Transactions.Count();
        }
    }

    public IEnumerable<TransactionDto> DepositTransaction
    {
        get
        {
            return Transactions.Where(o => o.Type.Equals("DEPOSIT", comparisonType: StringComparison.OrdinalIgnoreCase));
        }
    }

    public IEnumerable<TransactionDto> WithdrawTransaction
    {
        get
        {
            return Transactions.Where(o => o.Type.Equals("WITHDRAW", comparisonType: StringComparison.OrdinalIgnoreCase));
        }
    }
}