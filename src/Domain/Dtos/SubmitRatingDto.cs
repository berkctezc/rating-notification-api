namespace Domain.Dtos;

[ExcludeFromCodeCoverage]
public class SubmitRatingDto
{
    public Guid UserId { get; set; }

    public Guid ProviderId { get; set; }

    public uint Rating { get; set; }
}