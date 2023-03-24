namespace Application.Services;

public class RatingService : IRatingService
{
    private readonly IRatingRepository _repository;

    private readonly IMapper _mapper;

    private readonly IUserProvider _userProvider;

    private readonly IMessageProducer _messageProducer;

    public RatingService(IRatingRepository repository, IMapper mapper, IUserProvider userProvider, IMessageProducer messageProducer)
        => (_repository, _mapper, _userProvider, _messageProducer) = (repository, mapper, userProvider, messageProducer);


    public async Task<ApiResponseDto<SubmitRatingResponseModel>> SubmitRating(SubmitRatingRequestModel request)
    {
        try
        {
            var user = await _userProvider.GetUser();

            var response = await _repository.SubmitRating(new SubmitRatingDto
            {
                UserId = user.Id,
                ProviderId = request.ProviderId,
                Rating = request.Rating
            });

            if (response)
                _messageProducer.SendMessage(new RatingNotification
                {
                    Name = user.Name,
                    ProviderId = request.ProviderId,
                    Rating = request.Rating
                });

            return new ApiResponseDto<SubmitRatingResponseModel>
            {
                Data = new SubmitRatingResponseModel
                {
                    Name = user.Name,
                    ProviderId = request.ProviderId,
                    Rating = request.Rating
                }
            };
        }
        catch (Exception e)
        {
            throw new SubmitRatingException(e);
        }
    }

    public async Task<ApiResponseDto<GetAverageRatingOfProviderResponseModel>> GetAverageRatingOfProvider(GetAverageRatingOfProviderRequestModel request)
    {
        try
        {
            var dto = _mapper.Map<GetAverageRatingOfProviderDto>(request);

            var result = await _repository.GetAverageRatingOfProvider(dto);

            return new ApiResponseDto<GetAverageRatingOfProviderResponseModel>
            {
                Data = new GetAverageRatingOfProviderResponseModel
                {
                    ProviderId = dto.ProviderId,
                    AverageRating = result
                }
            };
        }
        catch (Exception e)
        {
            throw new GetAverageRatingOfProviderException(e);
        }
    }
}