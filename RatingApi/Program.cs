var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.RegisterRatingApiServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/rating", async ([FromServices] IRatingService service, [FromServices] IValidator<SubmitRatingRequestModel> validator, [FromBody] SubmitRatingRequestModel request) =>
{
    var validationResult = await validator.ValidateAsync(request);

    if (validationResult.IsValid)
    {
        var result = await service.SubmitRating(request);

        return Results.Ok(result);
    }

    var errors = validationResult.Errors.Select(x => x.ErrorMessage);

    return Results.BadRequest(errors);
});

app.MapGet("/get-avg", async ([FromServices] IRatingService service,
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
});

app.UseHttpsRedirection();

app.Run();