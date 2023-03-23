namespace Domain.Responses.Ratings;

[ExcludeFromCodeCoverage]
public class GetAverageRatingOfProviderResponseModel
{
    public Guid ProviderId { get; set; }

    public double AverageRating { get; set; }
}