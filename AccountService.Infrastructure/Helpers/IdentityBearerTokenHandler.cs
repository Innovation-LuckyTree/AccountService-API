using AccountService.Common.Interfaces;
using AccountService.Infrastructure.Interfaces;

namespace AccountService.Infrastructure.Helpers
{
    public class IdentityBearerTokenHandler : DelegatingHandler
    {
        /// <summary>
        /// The Identity API Client to retrieve the Auth token from.
        /// </summary>
        private readonly ICurrentUserService _currentUserService;
        private readonly IAppConfig _appConfig;

        public IdentityBearerTokenHandler(ICurrentUserService currentUserService, IAppConfig appConfig)
        {
            _currentUserService = currentUserService;
            _appConfig = appConfig;
        }

        /// <summary>
        /// If the Authorization header is missing, will call the Identity API and retrieve an auth token.
        /// Adds the Authorization header and then continues the HTTP Request.
        /// </summary>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("Authorization"))
            {
                request.Headers.Add("Authorization", $"Bearer {_currentUserService.AuthenticationBearer}");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("odata", "verbose");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
