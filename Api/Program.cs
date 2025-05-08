using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsApp.Api.Endpoints;
using NewsApp.Api.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddNewsApp();

builder.Services.AddOutputCache();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseOutputCache();

if (app.Environment.IsDevelopment())
{
    app
        .MapOpenApi()
        .CacheOutput();

    app.MapScalarApiReference();
}

app.MapEndpoints();

app.Run();
