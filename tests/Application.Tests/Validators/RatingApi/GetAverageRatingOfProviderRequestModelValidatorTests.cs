namespace Application.Tests.Validators.RatingApi;

public class GetAverageRatingOfProviderRequestModelValidatorTests
{
    private readonly GetAverageRatingOfProviderRequestModelValidator _sut;

    private readonly Guid _guid;


    public GetAverageRatingOfProviderRequestModelValidatorTests()
    {
        _sut = new GetAverageRatingOfProviderRequestModelValidator();

        _guid = Guid.NewGuid();
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenNullOrEmpty()
    {
        var model = new GetAverageRatingOfProviderRequestModel();

        var result = _sut.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ProviderId);
    }

    [Fact]
    public void Validator_ShouldNotHaveError_WhenValid()
    {
        var model = new GetAverageRatingOfProviderRequestModel
        {
            ProviderId = _guid
        };

        var result = _sut.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.ProviderId);
    }
}