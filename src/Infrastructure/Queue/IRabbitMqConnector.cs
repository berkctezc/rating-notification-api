namespace Infrastructure.Queue;

public interface IRabbitMqConnector
{
    IConnection GetConnection();
}

public class RabbitMqConnector : IRabbitMqConnector
{
    public IConnection GetConnection()
    {
        var factory = new ConnectionFactory
        {
            // todo: move to appsettings
            Uri = new Uri("amqp://berkcan:berkcan@localhost:5672"),
            VirtualHost = "/"
        };

        return factory.CreateConnection();
    }
}