namespace Infrastructure.Queue;

public class RabbitMqConnector : IRabbitMqConnector
{
    public IConnection GetConnection(string connectionString)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(connectionString),
            VirtualHost = "/"
        };

        return factory.CreateConnection();
    }
}