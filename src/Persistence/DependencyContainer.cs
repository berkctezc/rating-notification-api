using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

[ExcludeFromCodeCoverage]
public static class DependencyContainer
{
    public static IServiceCollection RegisterPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ArmutDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IRatingRepository, RatingRepository>()
            .AddScoped<IServiceProviderRepository, ServiceProviderRepository>();

        // SeedData.SeedAsync(connectionString).GetAwaiter().GetResult();

        return services;
    }
}