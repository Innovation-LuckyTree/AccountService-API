namespace AccountService.Infrastructure.Common.Models;

public class HttpCircuitBreaker
{
    public string DurationOfBreak { get; set; }
    public int ExceptionsAllowedBeforeBreaking { get; set; }
}
