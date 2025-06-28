namespace AccountService.Infrastructure.Clients.ConnectPay.Models.Requests;

public record CreateAccountRequest(string Name, string? Email, string MobileNumber);
