namespace NotificationApi;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterSettings(this IServiceCollection services)
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
}