var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.RegisterRatingApiServices();

var app = builder.Build();

if (!app.Environment.IsProduction())
    app.UseSwagger()
        .UseSwaggerUI();

app.MapRatingEndpoints()
    .UseMiddleware<LoggingMiddleware>()
    // .UseMiddleware<ExceptionMiddleware>()
    .UseMiddleware<RateLimitingMiddleware>()
    .UseHttpsRedirection();

app.Run();