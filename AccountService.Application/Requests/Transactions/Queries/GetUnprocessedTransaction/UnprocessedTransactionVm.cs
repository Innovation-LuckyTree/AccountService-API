namespace AccountService.Application.Requests.Transactions.Queries.GetUnprocessedTransaction;

public record UnprocessedTransactionVm(IEnumerable<long> transactions)
{
    public int Count
    {
        get
        {
            return transactions.Count();
        }
    }
}