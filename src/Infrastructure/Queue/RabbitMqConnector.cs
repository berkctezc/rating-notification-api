namespace Infrastructure.Queue;

public class RabbitMqConnector : IRabbitMqConnector
{
    public IConnection GetConnection(string connectionString)
    {
        var factory = new ConnectionFactory
        {
            // todo: move to appsettings
            Uri = new Uri(connectionString),
            VirtualHost = "/"
        };

        return factory.CreateConnection();
    }
}