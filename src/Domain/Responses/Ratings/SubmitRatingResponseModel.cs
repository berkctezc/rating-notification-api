namespace Domain.Responses.Ratings;

[ExcludeFromCodeCoverage]
public class SubmitRatingResponseModel
{
    public string Name { get; set; } = string.Empty;

    public Guid ProviderId { get; set; }

    public uint Rating { get; set; }
}