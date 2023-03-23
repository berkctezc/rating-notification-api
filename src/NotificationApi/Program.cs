var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .RegisterSettings()
    .RegisterConsumer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/notifications", ([FromServices] IMessageConsumer messageConsumer) =>
    {
        var newNotifs = messageConsumer.GetUnprocessedRatingNotifications();

        return Results.Ok(newNotifs);
    })
    .WithOpenApi(opt => new(opt)
    {
        Summary = "For getting unack notifications from queue",
        Description = "Get notifications endpoint"
    });

app.Run();