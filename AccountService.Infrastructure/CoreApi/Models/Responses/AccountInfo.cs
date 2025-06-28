namespace AccountService.Infrastructure.CoreApi.Models.Responses;

public class AccountInfo
{
    public long AccountInfoId { get; set; }
    public Guid AccountObjectId { get; set; }
    public Guid AccountCreditId { get; set; }
    public Guid AccountBonusId { get; set; }
    public Guid UserId { get; set; }
    public string PaymentAccount { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
    public string ProfilePath { get; set; }
    public string FullName { get; set; }
}
