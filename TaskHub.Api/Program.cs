using Scalar.AspNetCore;
using TaskHub.Api.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTaskHubServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();            
    app.MapScalarApiReference(); 
}

app.MapTaskHubEndpoints();

app.Run();
