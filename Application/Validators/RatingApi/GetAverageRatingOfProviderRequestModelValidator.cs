namespace Application.Validators.RatingApi;

public class GetAverageRatingOfProviderRequestModelValidator : AbstractValidator<GetAverageRatingOfProviderRequestModel>
{
    public GetAverageRatingOfProviderRequestModelValidator()
    {
        RuleFor(s => s.ProviderId)
            .NotEmpty()
            .NotNull();
    }
}