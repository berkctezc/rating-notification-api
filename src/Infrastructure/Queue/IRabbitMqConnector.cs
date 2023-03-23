namespace Infrastructure.Queue;

public interface IRabbitMqConnector
{
    IConnection GetConnection(string connectionString);
}