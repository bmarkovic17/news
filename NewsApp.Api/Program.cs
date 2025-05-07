using Microsoft.AspNetCore.Builder;
using NewsApp.Api.Endpoints;
using NewsApp.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddNewsApp();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();
