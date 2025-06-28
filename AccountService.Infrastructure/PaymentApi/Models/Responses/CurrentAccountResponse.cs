namespace AccountService.Infrastructure.PaymentApi.Models.Responses;
public class CurrentAccountResponse : ApiResponse<CurrentAccount>
{
}

public class CurrentAccount
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal MinimumCashIn { get; set; }
    public decimal MaximumCashIn { get; set; }
    public decimal Balance { get; set; }
}