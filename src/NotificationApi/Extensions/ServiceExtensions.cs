namespace NotificationApi.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceExtensions
{
    private static IServiceCollection RegisterSettings(this IServiceCollection services)
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

        return services;
    }

    public static IServiceCollection RegisterNotificationApiServices(this IServiceCollection services)
        => services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .RegisterSettings()
            .RegisterConsumer()
            .AddMiddlewares();

    private static IServiceCollection AddMiddlewares(this IServiceCollection services)
        => services
            .AddScoped<ExceptionMiddleware>()
            .AddSingleton<RateLimitingMiddleware>()
            .AddScoped<LoggingMiddleware>();

    public static void RegisterSerilog(this ILoggingBuilder logging)
        => logging.AddSerilog(
            new LoggerConfiguration()
                .WriteTo
                .File(
                    new RenderedCompactJsonFormatter(),
                    OperationSystemConstants.Personal("armut-notification-service"),
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true)
                .WriteTo
                .Console()
                .CreateLogger());
}