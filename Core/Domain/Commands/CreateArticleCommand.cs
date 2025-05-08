using System.Text.Json.Serialization;

namespace NewsApp.Core.Domain.Commands;

/// <summary>
/// Command to create a new article in the system.
/// Implements the ICommand interface as part of the CQRS pattern.
/// </summary>
public sealed class CreateArticleCommand : ICommand
{
    public string? Title { get; init; }

    public string? Content { get; init; }

    [JsonIgnore]
    public int? UserId { get; set; }
}
