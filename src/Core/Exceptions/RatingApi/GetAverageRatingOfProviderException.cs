namespace Core.Exceptions.RatingApi;

public class GetAverageRatingOfProviderException : Exception
{
    public GetAverageRatingOfProviderException(Exception? innerExp) :
        base(innerExp?.Message)
    {
    }
}