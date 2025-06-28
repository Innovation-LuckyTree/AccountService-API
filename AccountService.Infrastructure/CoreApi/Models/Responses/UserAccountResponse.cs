namespace AccountService.Infrastructure.CoreApi.Models.Responses;

public class UserAccountResponse
{
    public long AccountInfoId { get; set; }
    public Guid AccountObjectId { get; set; }
    public Guid AccountCreditId { get; set; }
    public string MobileNumber { get; set; }
}
