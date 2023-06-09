var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
builder.Logging.RegisterSerilog();

services.RegisterNotificationApiServices();

var app = builder.Build();

if (!app.Environment.IsProduction())
    app.UseSwagger()
        .UseSwaggerUI();

app.MapNotificationEndpoints()
    .UseMiddleware<LoggingMiddleware>()
    .UseMiddleware<ExceptionMiddleware>()
    .UseMiddleware<RateLimitingMiddleware>()
    .UseHttpsRedirection();

app.Run();