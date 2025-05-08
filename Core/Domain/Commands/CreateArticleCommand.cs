using System.Text.Json.Serialization;

namespace NewsApp.Core.Domain.Commands;

public sealed class CreateArticleCommand : ICommand
{
    public string? Title { get; init; }

    public string? Content { get; init; }

    [JsonIgnore]
    public int? UserId { get; set; }
}
