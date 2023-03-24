var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var logging = builder.Logging;

logging.AddSerilog(
    new LoggerConfiguration()
        .WriteTo
        .File(
            new RenderedCompactJsonFormatter(),
            OperationSystemConstants.Personal("armut-rating-service"),
            rollingInterval: RollingInterval.Day,
            rollOnFileSizeLimit: true)
        .WriteTo
        .Console()
        .CreateLogger());

services.RegisterRatingApiServices();

var app = builder.Build();

if (!app.Environment.IsProduction())
    app.UseSwagger()
        .UseSwaggerUI();

app.MapRatingEndpoints()
    .UseMiddleware<LoggingMiddleware>()
    .UseMiddleware<ExceptionMiddleware>()
    .UseMiddleware<RateLimitingMiddleware>()
    .UseHttpsRedirection();

app.Run();