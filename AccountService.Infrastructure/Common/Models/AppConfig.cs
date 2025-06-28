using AccountService.Infrastructure.Interfaces;

namespace AccountService.Infrastructure.Common.Models;

public class AppConfig : IAppConfig
{
    public string AppId { get; set; }
    public int GameId { get; set; }
    public JwtConfig JwtConfig { get; set; }
    public ApiClientConfig CoreIdentityApiClient { get; set; }
    public ApiClientConfig CoreApiClient { get; set; }
    public ApiClientConfig PaymentApiClient { get; set; }
    public ApiClientConfig WalletApiClient { get; set; }
}
