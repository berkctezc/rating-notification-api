namespace RatingApi.Extensions;

[ExcludeFromCodeCoverage]
public static class Endpoints
{
    public static WebApplication MapRatingEndpoints(this WebApplication app)
    {
        app.MapPost("/rating", async (
                [FromServices] IRatingService service,
                [FromServices] IValidator<SubmitRatingRequestModel> validator,
                [FromBody] SubmitRatingRequestModel request) =>
            {
                var validationResult = await validator.ValidateAsync(request);

                if (validationResult.IsValid)
                {
                    var result = await service.SubmitRating(request);

                    return Results.Ok(result);
                }

                var errors = validationResult.Errors.Select(x => x.ErrorMessage);

                return Results.BadRequest(errors);
            })
            .WithTags("rating")
            .WithOpenApi(opt => new(opt)
            {
                Summary = "For user to post/update ratings for service provider",
                Description = "Submit rating endpoint"
            });

        app.MapGet("/get-avg", async (
                [FromServices] IRatingService service,
                [FromServices] IValidator<GetAverageRatingOfProviderRequestModel> validator,
                [FromQuery] Guid providerId) =>
            {
                var request = new GetAverageRatingOfProviderRequestModel
                {
                    ProviderId = providerId
                };

                var validationResult = await validator.ValidateAsync(request);

                if (validationResult.IsValid)
                    return Results.Ok(await service.GetAverageRatingOfProvider(request));

                var errors = validationResult.Errors.Select(x => x.ErrorMessage);

                return Results.BadRequest(errors);
            })
            .WithTags("rating")
            .WithOpenApi(opt => new(opt)
            {
                Summary = "For getting average rating of a specified service provider",
                Description = "Get average rating endpoint"
            });

        return app;
    }
}