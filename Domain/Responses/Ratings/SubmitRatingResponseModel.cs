namespace Domain.Responses.Ratings;

public class SubmitRatingResponseModel
{
    public string Name { get; set; }

    public Guid ProviderId { get; set; }

    public uint Rating { get; set; }
}