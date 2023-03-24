namespace Application.Abstract;

public interface IRatingService
{
    /// <summary>
    /// for users to submit ratings for service providers
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ApiResponseDto<SubmitRatingResponseModel>> SubmitRating(SubmitRatingRequestModel request);

    /// <summary>
    /// for getting average rating of the specified user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ApiResponseDto<GetAverageRatingOfProviderResponseModel>> GetAverageRatingOfProvider(GetAverageRatingOfProviderRequestModel request);
}