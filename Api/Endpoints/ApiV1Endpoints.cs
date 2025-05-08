using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace NewsApp.Api.Endpoints;

/// <summary>
/// Provides configuration for the version 1 API endpoints.
/// Organizes endpoints into public and secured groups.
/// </summary>
internal static class ApiV1Endpoints
{
    private const string Prefix = "api/v1";

    public static void MapEndpoints(this IEndpointRouteBuilder builder)
    {
        // Public API endpoints
        _ = builder
            .MapGroup(Prefix)
            .MapPublicArticleEndpoints();

        // Secured API endpoints
        _ = builder
            .MapGroup(Prefix)
            .RequireAuthorization()
            .MapSecuredArticleEndpoints();
    }
}
