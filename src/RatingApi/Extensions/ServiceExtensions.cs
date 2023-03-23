namespace RatingApi.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceExtensions
{
    private static IConfigurationRoot BuildConfiguration(this IServiceCollection services)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var sectionStartupSettings = builder.GetSection(nameof(ServiceSettings));

        services.Configure<ServiceSettings>(sectionStartupSettings);

        return builder;
    }

    public static IServiceCollection RegisterRatingApiServices(this IServiceCollection services)
    {
        var confRoot = services.BuildConfiguration();

        var settings = confRoot.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>() ?? throw new ConfigurationReadException(null);

        services
            .RegisterConsumer()
            .RegisterPublisher()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddMiddlewares()
            .RegisterPersistence(settings.ConnectionString)
            .RegisterApplication();

        return services;
    }

    private static IServiceCollection AddMiddlewares(this IServiceCollection services)
        => services
            .AddScoped<ExceptionMiddleware>()
            .AddSingleton<RateLimitingMiddleware>()
            .AddScoped<LoggingMiddleware>();
}