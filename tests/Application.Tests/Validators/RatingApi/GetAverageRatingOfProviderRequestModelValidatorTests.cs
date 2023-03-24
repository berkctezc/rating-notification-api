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
        // Arrange
        var model = new GetAverageRatingOfProviderRequestModel();

        // Act
        var result = _sut.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProviderId);
    }

    [Fact]
    public void Validator_ShouldNotHaveError_WhenValid()
    {
        // Arrange
        var model = new GetAverageRatingOfProviderRequestModel
        {
            ProviderId = _guid
        };

        // Act
        var result = _sut.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.ProviderId);
    }
}