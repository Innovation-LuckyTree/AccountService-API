namespace AccountService.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string AuthenticationBearer { get; }
        Guid CompanyId { get; }
    }
}
