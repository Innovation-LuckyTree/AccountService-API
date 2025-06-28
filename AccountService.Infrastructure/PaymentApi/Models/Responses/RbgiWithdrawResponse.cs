namespace AccountService.Infrastructure.PaymentApi.Models.Responses;

public class RbgiWithdrawResponse : ApiResponse<RbgiWithdrawData>
{

}

public class RbgiWithdrawData
{
    public int Code { get; set; }
    public string? Message { get; set; }
    public string? Status { get; set; }
    public string? StatusDesc { get; set; }
    public string? ReferenceId { get; set; }
}