using System.Security.Authentication;

namespace AccountService.Infrastructure.Helpers
{
    public class PrimaryHttpClientHandlerFactory
    {
        public static HttpClientHandler CreateHttpClientHandler() => new HttpClientHandler { SslProtocols = SslProtocols.Tls12 };
    }
}
