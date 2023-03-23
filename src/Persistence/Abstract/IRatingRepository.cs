namespace Persistence.Abstract;

public interface IRatingRepository
{
    Task<bool> SubmitRating(SubmitRatingDto dto);
    Task<double> GetAverageRatingOfProvider(GetAverageRatingOfProviderDto dto);
}