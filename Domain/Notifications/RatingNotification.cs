namespace Domain.Notifications;

public class RatingNotification
{
    public string Name { get; set; }

    public Guid ProviderId { get; set; }

    public uint Rating { get; set; }
}