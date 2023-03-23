namespace NotificationApi.Extensions;

public static class Endpoints
{
    public static WebApplication MapNotificationEndpoints(this WebApplication app)
    {
        app.MapGet("/notifications", ([FromServices] IMessageConsumer messageConsumer) =>
            {
                var newNotifs = messageConsumer.GetUnprocessedRatingNotifications();

                return Results.Ok(newNotifs);
            })
            .WithTags("notification")
            .WithOpenApi(opt => new(opt)
            {
                Summary = "For getting unack notifications from queue",
                Description = "Get notifications endpoint"
            });

        return app;
    }
}