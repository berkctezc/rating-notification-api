namespace Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GetAverageRatingOfProviderRequestModel, GetAverageRatingOfProviderDto>()
            .ReverseMap();

        CreateMap<RatingNotification, SubmitRatingRequestModel>()
            .ReverseMap();
    }
}