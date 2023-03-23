using Infrastructure.Queue;

namespace Infrastructure;

public static class DependencyContainer
{
    public static IServiceCollection RegisterPublisher(this IServiceCollection services)
    {
        services
            .AddScoped<IMessageProducer, MessageProducer>()
            .AddSingleton<IRabbitMqConnector, RabbitMqConnector>();

        return services;
    }

    public static IServiceCollection RegisterConsumer(this IServiceCollection services)
    {
        services
            .AddScoped<IMessageConsumer, MessageConsumer>()
            .AddSingleton<IRabbitMqConnector, RabbitMqConnector>();

        return services;
    }
}