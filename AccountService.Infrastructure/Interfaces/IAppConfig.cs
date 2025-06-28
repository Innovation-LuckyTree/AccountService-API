using AccountService.Infrastructure.Common.Models;

namespace AccountService.Infrastructure.Interfaces
{
    public interface IAppConfig
    {
        string AppId { get; set; }
        int GameId { get; set; }

        JwtConfig JwtConfig { get; set; }
        ApiClientConfig CoreIdentityApiClient { get; set; }
        ApiClientConfig CoreApiClient { get; set; }
        ApiClientConfig PaymentApiClient { get; set; }
        ApiClientConfig WalletApiClient { get; set; }
    }
}
