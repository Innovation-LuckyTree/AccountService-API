using AccountService.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountService.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PaymentDataSourceDbContext>(opts => opts.UseNpgsql(connectionString));
        services.AddScoped<IPaymentDataSourceDbContext>(provider => provider.GetService<PaymentDataSourceDbContext>());

        return services;
    }
}
