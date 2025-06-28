namespace AccountService.Infrastructure.Common.Models;

public class ApiPolicyConfig
{
    public HttpCircuitBreaker HttpCircuitBreaker { get; set; }
    public HttpRetry HttpRetry { get; set; }    
}
