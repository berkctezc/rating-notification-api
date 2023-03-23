namespace Infrastructure.Queue;

public class MessageProducer : IMessageProducer
{
    private readonly IConnection _connection;

    public MessageProducer(IRabbitMqConnector connection)
        => _connection = connection.GetConnection();

    public void SendMessage<T>(T message)
    {
        using var channel = _connection.CreateModel();

        channel.QueueDeclare("ratings", durable: true, exclusive: false);

        var jsonStr = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonStr);

        channel.BasicPublish("", "ratings", body: body);
    }
}