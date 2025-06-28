using AccountService.Infrastructure.CoreApi;
using AccountService.Infrastructure.Helpers;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.PaymentApi;
using AccountService.Infrastructure.WalletApi;
using Microsoft.Extensions.DependencyInjection;

namespace AccountService.Infrastructure
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddTransient<IdentityBearerTokenHandler>();

            services.AddHttpClient<ICoreApiService, CoreApiService>()
                .ConfigurePrimaryHttpMessageHandler(PrimaryHttpClientHandlerFactory.CreateHttpClientHandler)
                .AddHttpMessageHandler<IdentityBearerTokenHandler>();

            services.AddHttpClient<IPaymentApiService, PaymentApiService>()
                .ConfigurePrimaryHttpMessageHandler(PrimaryHttpClientHandlerFactory.CreateHttpClientHandler)
                .AddHttpMessageHandler<IdentityBearerTokenHandler>();

            services.AddHttpClient<IWalletApiService, WalletApiService>()
                .ConfigurePrimaryHttpMessageHandler(PrimaryHttpClientHandlerFactory.CreateHttpClientHandler)
                .AddHttpMessageHandler<IdentityBearerTokenHandler>();

            return services;
        }
    }
}