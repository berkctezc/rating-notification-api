namespace Application.Validators.RatingApi;

public class SubmitRatingRequestModelValidator : AbstractValidator<SubmitRatingRequestModel>
{
    public SubmitRatingRequestModelValidator()
    {
        RuleFor(s => (int) s.Rating)
            .InclusiveBetween(1, 10)
            .NotEmpty()
            .NotNull();

        RuleFor(s => s.ProviderId)
            .NotEmpty()
            .NotNull();
    }
}