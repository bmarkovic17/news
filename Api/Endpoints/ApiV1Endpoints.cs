using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace NewsApp.Api.Endpoints;

internal static class ApiV1Endpoints
{
    private const string Prefix = "api/v1";

    public static void MapEndpoints(this IEndpointRouteBuilder builder)
    {
        // Public API endpoints
        _ = builder
            .MapGroup(Prefix)
            .MapPublicArticleEndpoints();

        // Secure API endpoints
        _ = builder
            .MapGroup(Prefix)
            .RequireAuthorization()
            .MapSecuredArticleEndpoints();
    }
}
