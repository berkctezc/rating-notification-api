namespace Core.Tests.Middlewares;

public class RateLimitingMiddlewareTests
{
    private readonly DefaultHttpContext _context;

    public RateLimitingMiddlewareTests()
    {
        _context = new DefaultHttpContext
        {
            Connection =
            {
                RemoteIpAddress = new IPAddress(new byte[] {127, 0, 0, 1})
            }
        };
    }

    [Fact]
    public async void Middleware_LimitNotExceeded_ShouldReturn200()
    {
        // Arrange
        var sut = new RateLimitingMiddleware();

        var nextExecuted = false;

        var next = new RequestDelegate(_ =>
        {
            nextExecuted = true;
            return Task.CompletedTask;
        });

        // Act
        await sut.InvokeAsync(_context, next);

        // Assert
        nextExecuted.Should().BeTrue();
        _context.Response.StatusCode.Should().Be(200);
    }

    [Fact]
    public async void Middleware_LimitExceeded_ShouldReturn429()
    {
        // Arrange
        var sut = new RateLimitingMiddleware();

        for (var i = 0; i < 10; i++)
            await sut.InvokeAsync(_context, _ => Task.CompletedTask);

        // Act
        await sut.InvokeAsync(_context, _ => Task.CompletedTask);

        // Assert
        _context.Response.StatusCode.Should().Be(429);
    }
}