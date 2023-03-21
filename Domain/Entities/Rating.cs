namespace Domain.Entities;

public class Rating : IEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ServiceProviderId { get; set; }

    public DateTime Date { get; set; }

    [Range(1,10)]
    public uint RatingNumber { get; set; }
}