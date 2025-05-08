using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewsApp.Api.Endpoints;
using NewsApp.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddNewsApp();

builder.Services.AddOutputCache();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseOutputCache();

app.MapEndpoints();

app.Run();
