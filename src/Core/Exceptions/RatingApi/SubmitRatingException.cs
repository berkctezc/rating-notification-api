namespace Core.Exceptions.RatingApi;

public class SubmitRatingException : Exception
{
    public SubmitRatingException(Exception? innerExp) :
        base(innerExp?.Message)
    {
    }
}