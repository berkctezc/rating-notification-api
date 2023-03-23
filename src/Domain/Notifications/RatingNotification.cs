namespace Domain.Notifications;

[ExcludeFromCodeCoverage]
public class RatingNotification
{
    public string Name { get; set; } = string.Empty;

    public Guid ProviderId { get; set; }

    public uint Rating { get; set; }
}