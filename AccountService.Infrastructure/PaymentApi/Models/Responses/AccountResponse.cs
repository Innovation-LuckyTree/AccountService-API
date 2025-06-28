namespace AccountService.Infrastructure.PaymentApi.Models.Responses;

public class AccountResponse : ApiResponse<AccountDto>
{
}

public class AccountDto
{
    public string Id { get; set; }
    public string Name { get; set; }

    public string Email { get; set; }
    public string MobileNumber { get; set; }

    public long TransactionCount { get; set; }
    public DateTime CreatedAt { get; set; }

    public DateTime UpdateAt { get; set; }
}
