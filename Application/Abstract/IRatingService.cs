using Core.Dtos;

namespace Application.Abstract;

public interface IRatingService
{
  Task<ApiResponseDto<SubmitRatingResponseModel>> SubmitRating(SubmitRatingRequestModel request);

  Task<ApiResponseDto<GetAverageRatingOfProviderResponseModel>> GetAverageRatingOfProvider(GetAverageRatingOfProviderRequestModel request);
}