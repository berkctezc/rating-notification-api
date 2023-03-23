using Infrastructure;
using Infrastructure.Queue;
using Microsoft.AspNetCore.Mvc;
using NotificationApi;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.RegisterSettings();
services.RegisterConsumer();

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
});

app.Run();