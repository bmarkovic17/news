namespace NewsApp.Core.Commands;

public sealed class CreateArticleCommand : ICommand
{
    public string? Title { get; init; }

    public string? Content { get; init; }
}
