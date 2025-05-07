using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace NewsApp.Api.Endpoints;

internal static class ApiV1Endpoints
{
    private const string Prefix = "api/v1";

    public static void MapEndpoints(this IEndpointRouteBuilder builder)
    {
        _ = builder
            .MapGroup(Prefix)
            .MapArticleEndpoints();
    }
}
