namespace Domain.Responses.Ratings;

public class GetAverageRatingOfProviderResponseModel
{
    public Guid ProviderId { get; set; }

    public double AverageRating { get; set; }
}