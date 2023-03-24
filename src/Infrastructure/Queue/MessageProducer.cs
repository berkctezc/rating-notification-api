namespace Infrastructure.Queue;

public class MessageProducer : IMessageProducer
{
    private readonly IConnection _connection;

    public MessageProducer(IRabbitMqConnector connection, IOptions<Domain.Settings.RatingApi.ServiceSettings> options)
        => _connection = connection.GetConnection(options.Value.QConnectionString);

    public void SendMessage<T>(T message)
    {
        try
        {
            Log.Logger.Information("{Message} pushed", message);

            using var channel = _connection.CreateModel();

            channel.QueueDeclare("ratings", durable: true, exclusive: false);

            var jsonStr = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonStr);

            channel.BasicPublish("", "ratings", body: body);
        }
        catch (Exception e)
        {
            Log.Logger.Error("{Message} could not be pushed with Exception: {Exception}", message, e);
        }
    }
}