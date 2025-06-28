namespace AccountService.Application.Requests.Transactions.Queries.GetPendingNotification;

public record PendingNotificationVm(IEnumerable<long> transactions)
{
    public int Count
    {
        get
        {
            return transactions.Count();
        }
    }
}