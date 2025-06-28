namespace AccountService.Application.Requests.Transactions.Queries.GetTransactionNotification;

public class TransactionNotification
{
    public string Type { get; set; }
    public string NotificationName { get; set; }
    public List<NotificationAccount> Accounts { get; set; }
    public List<string> Args { get; set; }
}

public class NotificationAccount
{
    public string Name { get; set; }
    public Guid UserId { get; set; }
    public long AccountId { get; set; }
    public int UserTypeId { get; set; }
}
