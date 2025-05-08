using System;
using System.Threading.Tasks;
using NewsApp.Core.Domain.Entities.ArticleEntity;

namespace NewsApp.UnitTests.Core.Domain;

public class ArticleTests
{
    // Number of milliseconds to check for timestamp changes (e.g. an update has happened
    // if the Modified timestamp is less than current timestamp minus this interval
    private const int MillisecondsForTimestampComparison = -10;
    private readonly TimeSpan _millisecondsForTimestampComparisonDelay =
        TimeSpan.FromMilliseconds(-MillisecondsForTimestampComparison);

    [Fact]
    public void Title_Create_Valid_ReturnsSuccessResult()
    {
        // Arrange

        // Act
        var result = Title.Create("Article title 1");

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Value);
    }

    [Theory]
    [ClassData(typeof(CreateInvalidTitleData))]
    public void Title_Create_Invalid_ReturnsFailResult(string? title)
    {
        // Arrange

        // Act
        var result = Title.Create(title);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotEmpty(result.Errors.SelectMany(error => error.Value));
        Assert.Null(result.Value);
    }

    [Theory]
    [ClassData(typeof(CreateValidContentData))]
    public void Content_Create_Valid_ReturnsSuccessResult(string? content)
    {
        // Arrange

        // Act
        var result = Content.Create(content);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Article_Create_Valid_ReturnsSuccessResult()
    {
        // Arrange

        // Act
        var result = Article.Create("Article title", null);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Value);
        Assert.NotNull(result.Value.Title.Value);
        Assert.Equal(ArticleStatus.Draft, result.Value.Status);
        Assert.True(result.Value.Created > DateTimeOffset.UtcNow.AddMilliseconds(MillisecondsForTimestampComparison));
        Assert.Null(result.Value.Modified);
        Assert.Null(result.Value.Published);
    }

    [Theory]
    [ClassData(typeof(CreateInvalidArticleData))]
    public void Article_Create_Invalid_ReturnsFailResult(string? title, string? content)
    {
        // Arrange

        // Act
        var result = Article.Create(title, content);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotEmpty(result.Errors.SelectMany(error => error.Value));
        Assert.Null(result.Value);
    }

    [Fact]
    public void Article_Update_Valid_ReturnsSuccessResult()
    {
        // Arrange
        const string newTitle = "Article title 2";
        const string newContent = "Sample content";
        var article = Article.Create("Article title", null).Value!;

        // Act
        var result = article.Update(newTitle, newContent);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
        Assert.Equal(newTitle, article.Title.Value);
        Assert.Equal(newContent, article.Content.Value);
        Assert.True(article.Modified > DateTimeOffset.UtcNow.AddMilliseconds(MillisecondsForTimestampComparison));
    }

    [Fact]
    public void Article_Update_NoChanges_ReturnsSuccessResult()
    {
        // Arrange
        var article = Article.Create("Article title", null).Value!;
        var sameTitle = article.Title.Value;
        var sameContent = article.Content.Value;

        // Act
        var result = article.Update(sameTitle, sameContent);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
        Assert.Equal(sameTitle, article.Title.Value);
        Assert.Equal(sameContent, article.Content.Value);
        Assert.Null(article.Modified);
    }

    [Fact]
    public void Article_Publish_Valid_ReturnsSuccessResult()
    {
        // Arrange
        var article = Article.Create("Article title", null).Value!;

        // Act
        var result = article.Publish();

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
        Assert.Equal(ArticleStatus.Published, article.Status);
        Assert.True(article.Modified > DateTimeOffset.UtcNow.AddMilliseconds(MillisecondsForTimestampComparison));
        Assert.True(article.Published > DateTimeOffset.UtcNow.AddMilliseconds(MillisecondsForTimestampComparison));
    }

    [Fact]
    public async Task Article_Publish_NoChanges_ReturnsSuccessResult()
    {
        // Arrange
        var article = Article.Create("Article title", null).Value!;
        _ = article.Publish();
        // Wait for the comparison delay to run out
        await Task.Delay(_millisecondsForTimestampComparisonDelay);

        // Act
        var result = article.Publish();

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
        Assert.Equal(ArticleStatus.Published, article.Status);
        Assert.False(article.Modified > DateTimeOffset.UtcNow.AddMilliseconds(MillisecondsForTimestampComparison));
        Assert.False(article.Published > DateTimeOffset.UtcNow.AddMilliseconds(MillisecondsForTimestampComparison));
    }

    [Fact]
    public void Article_Unpublish_Valid_ReturnsSuccessResult()
    {
        // Arrange
        var article = Article.Create("Article title", null).Value!;
        _ = article.Publish();

        // Act
        var result = article.Unpublish();

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
        Assert.Equal(ArticleStatus.Unpublished, article.Status);
        Assert.True(article.Modified > DateTimeOffset.UtcNow.AddMilliseconds(MillisecondsForTimestampComparison));
        Assert.Null(article.Published);
    }

    [Fact]
    public async Task Article_Unpublish_NoChanges_ReturnsSuccessResult()
    {
        // Arrange
        var article = Article.Create("Article title", null).Value!;
        _ = article.Publish();
        _ = article.Unpublish();
        // Wait for the comparison delay to run out
        await Task.Delay(_millisecondsForTimestampComparisonDelay);

        // Act
        var result = article.Unpublish();

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
        Assert.Equal(ArticleStatus.Unpublished, article.Status);
        Assert.False(article.Modified > DateTimeOffset.UtcNow.AddMilliseconds(MillisecondsForTimestampComparison));
        Assert.Null(article.Published);
    }

    [Fact]
    public void Article_Unpublish_Invalid_ReturnsFailResult()
    {
        // Arrange
        var article = Article.Create("Article title", null).Value!;

        // Act
        var result = article.Unpublish();

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotEmpty(result.Errors.SelectMany(error => error.Value));
        Assert.Equal(ArticleStatus.Draft, article.Status);
        Assert.Null(article.Modified);
        Assert.Null(article.Published);
    }
}

public class CreateInvalidTitleData : TheoryData<string?>
{
    public CreateInvalidTitleData()
    {
        Add(null);
        Add(string.Empty);
        Add("   ");
        Add(Enumerable.Repeat("a", 51).Aggregate((result, current) => result + current));
    }
}

public class CreateValidContentData : TheoryData<string?>
{
    public CreateValidContentData()
    {
        Add(null);
        Add(string.Empty);
        Add("   ");
        Add("Sample content");
    }
}

public class CreateInvalidArticleData : TheoryData<string?, string?>
{
    private readonly string _longTitle =
        Enumerable.Repeat("a", 51).Aggregate((result, current) => result + current);

    public CreateInvalidArticleData()
    {
        Add(null, null);
        Add(null, string.Empty);
        Add(null, "   ");
        Add(string.Empty, null);
        Add(string.Empty, string.Empty);
        Add(string.Empty, "   ");
        Add("   ", null);
        Add("   ", string.Empty);
        Add("   ", "   ");
        Add(_longTitle, null);
        Add(_longTitle, string.Empty);
        Add(_longTitle, "   ");
    }
}
