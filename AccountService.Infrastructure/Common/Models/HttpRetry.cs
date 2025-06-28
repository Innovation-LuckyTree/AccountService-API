namespace AccountService.Infrastructure.Common.Models;

public class HttpRetry
{
    public int BackoffPower { get; set; }
    public int Count { get; set; }
}
