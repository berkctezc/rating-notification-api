namespace Infrastructure.Queue;

public class MessageConsumer : IMessageConsumer
{
    private readonly IConnection _connection;

    public MessageConsumer(IRabbitMqConnector connection, IOptions<Domain.Settings.NotificationApi.ServiceSettings> options)
        => _connection = connection.GetConnection(options.Value.QConnectionString);

    public IEnumerable<RatingNotification> GetUnprocessedRatingNotifications()
    {
        var channel = _connection.CreateModel();

        var getResult = channel.BasicGet("ratings", false);

        var newNotifications = new Queue<RatingNotification>();

        if (getResult is null)
            return newNotifications;

        var count = getResult.MessageCount + 1;

        for (var i = 0; i < count; i++)
        {
            var body = getResult.Body.ToArray();

            var message = Encoding.UTF8.GetString(body);

            newNotifications.Enqueue(JsonSerializer.Deserialize<RatingNotification>(message) ?? new RatingNotification());

            var deliveryTag = getResult.DeliveryTag;
            channel.BasicAck(deliveryTag, false);

            getResult = channel.BasicGet("ratings", false);
        }

        return newNotifications;
    }
}