namespace Infrastructure.Queue;

public interface IMessageConsumer
{
    IEnumerable<RatingNotification> GetUnprocessedRatingNotifications();
}