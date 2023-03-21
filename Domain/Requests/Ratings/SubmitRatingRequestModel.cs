namespace Domain.Requests.Ratings;

public class SubmitRatingRequestModel
{
    public Guid ProviderId { get; set; }

    public uint Rating { get; set; }
}