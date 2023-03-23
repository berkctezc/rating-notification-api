using AutoFixture;

namespace Application.Tests.Validators.RatingApi;

public class SubmitRatingRequestModelValidatorTests
{
    private readonly SubmitRatingRequestModelValidator _sut;

    private readonly Guid _guid;

    private readonly uint _validNumber;

    private readonly uint _invalidNumber;

    public SubmitRatingRequestModelValidatorTests()
    {
        var fixture = new Fixture();

        _sut = new SubmitRatingRequestModelValidator();

        _guid = Guid.NewGuid();

        _validNumber = fixture.Create<uint>() % 10 + 1;

        _invalidNumber = fixture.Create<uint>() + 10;
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenNullOrEmpty()
    {
        var model = new SubmitRatingRequestModel();

        var result = _sut.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ProviderId);
        result.ShouldHaveValidationErrorFor(x => x.Rating);
    }

    [Fact]
    public void Validator_ShouldNotHaveError_WhenValid()
    {
        var model = new SubmitRatingRequestModel
        {
            ProviderId = _guid,
            Rating = _validNumber
        };

        var result = _sut.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.ProviderId);
        result.ShouldNotHaveValidationErrorFor(x => x.Rating);
    }

    [Fact]
    public void Validator_ShouldNotHaveError_WhenInvalidRating()
    {
        var model = new SubmitRatingRequestModel
        {
            ProviderId = _guid,
            Rating = _invalidNumber
        };

        var result = _sut.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.ProviderId);
        result.ShouldHaveValidationErrorFor(x => x.Rating);
    }
}