namespace Application.Tests.Services;

public class RatingServiceTests
{
    private readonly RatingService _sut;

    private readonly SubmitRatingRequestModel _submitRatingRequest;

    private readonly GetAverageRatingOfProviderRequestModel _getAverageRatingRequest;

    private readonly IUserProvider _mockUserProvider;

    private readonly IRatingRepository _mockRatingRepository;

    public RatingServiceTests()
    {
        var fixture = new Fixture();
        _submitRatingRequest = fixture.Create<SubmitRatingRequestModel>();
        _getAverageRatingRequest = fixture.Create<GetAverageRatingOfProviderRequestModel>();

        _mockRatingRepository = Substitute.For<IRatingRepository>();
        _mockRatingRepository.SubmitRating(Arg.Any<SubmitRatingDto>())
            .Returns(true);
        _mockRatingRepository.GetAverageRatingOfProvider(Arg.Any<GetAverageRatingOfProviderDto>())
            .Returns(fixture.Create<double>() % 10 + 1);

        var mockMapper = Substitute.For<IMapper>();
        mockMapper.Map<GetAverageRatingOfProviderDto>(Arg.Any<object>())
            .Returns(fixture.Create<GetAverageRatingOfProviderDto>());

        _mockUserProvider = Substitute.For<IUserProvider>();
        _mockUserProvider.GetUser()
            .Returns(fixture.Create<User>());

        var mockMessageProducer = Substitute.For<IMessageProducer>();

        _sut = new RatingService(_mockRatingRepository, mockMapper, _mockUserProvider, mockMessageProducer);
    }


    [Fact]
    public async void SubmitRating_ShouldNotThrow()
    {
        // Act
        var act = async () => await _sut.SubmitRating(_submitRatingRequest);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async void SubmitRating_ShouldThrow_WhenUserProviderThrows()
    {
        // Arrange
        _mockUserProvider.GetUser().Throws(new Exception());

        // Act
        var act = async () => await _sut.SubmitRating(_submitRatingRequest);

        // Assert
        await act.Should().ThrowAsync<SubmitRatingException>();
    }

    [Fact]
    public async void GetAverageRatingOfProvider_ShouldNotThrow()
    {
        // Act
        var act = async () => await _sut.GetAverageRatingOfProvider(_getAverageRatingRequest);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async void GetAverageRatingOfProvider_ShouldThrow_WhenRatingRepositoryThrows()
    {
        // Arrange
        _mockRatingRepository.GetAverageRatingOfProvider(Arg.Any<GetAverageRatingOfProviderDto>()).Throws(new Exception());

        // Act
        var act = async () => await _sut.GetAverageRatingOfProvider(_getAverageRatingRequest);

        // Assert
        await act.Should().ThrowAsync<GetAverageRatingOfProviderException>();
    }
}