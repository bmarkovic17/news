using NewsApp.Core.SharedKernel;

namespace NewsApp.UnitTests.Core.SharedKernel;

public class ResultTests
{
    [Fact]
    public void Success_NoErrors_ReturnsSuccessResult()
    {
        // Arrange

        // Act
        var result = Result.Success();

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Fail_WithoutErrors_ReturnsFailResult()
    {
        // Arrange

        // Act
        var result = Result.Fail(new Dictionary<string, ICollection<string>>());

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotEmpty(result.Errors.SelectMany(error => error.Value));
    }

    [Fact]
    public void Fail_WithErrors_ReturnsFailResult()
    {
        // Arrange

        // Act
        var result = Result.Fail(new Dictionary<string, ICollection<string>>
        {
            [string.Empty] = ["error"]
        });

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotEmpty(result.Errors.SelectMany(error => error.Value));
    }

    [Fact]
    public void Create_NoErrors_ReturnsSuccessResult()
    {
        // Arrange

        // Act
        var result = Result.Create(new Dictionary<string, ICollection<string>>());

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Create_WithErrors_ReturnsFailResult()
    {
        // Arrange

        // Act
        var result = Result.Create(new Dictionary<string, ICollection<string>>
        {
            [string.Empty] = ["error"]
        });

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotEmpty(result.Errors.SelectMany(error => error.Value));
    }

    [Fact]
    public void Generic_Success_NoErrors_ReturnsSuccessResult()
    {
        // Arrange

        // Act
        var result = Result<object>.Success(new object());

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public void Generic_Fail_WithoutErrors_ReturnsFailResult()
    {
        // Arrange

        // Act
        var result = Result<object>.Fail(new Dictionary<string, ICollection<string>>());

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotEmpty(result.Errors.SelectMany(error => error.Value));
        Assert.Null(result.Value);
    }

    [Fact]
    public void Generic_Fail_WithErrors_ReturnsFailResult()
    {
        // Arrange

        // Act
        var result = Result<object>.Fail(new Dictionary<string, ICollection<string>>
        {
            [string.Empty] = ["error"]
        });

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotEmpty(result.Errors.SelectMany(error => error.Value));
        Assert.Null(result.Value);
    }

    [Fact]
    public void Generic_Create_NoErrors_ReturnsSuccessResult()
    {
        // Arrange

        // Act
        var result = Result<object>.Create(
            valueFactory: () => new object(),
            new Dictionary<string, ICollection<string>>());

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Generic_Create_WithErrors_ReturnsFailResult()
    {
        // Arrange

        // Act
        var result = Result<object>.Create(
            valueFactory: () => new object(),
            new Dictionary<string, ICollection<string>>
            {
                [string.Empty] = ["error"]
            });

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotEmpty(result.Errors.SelectMany(error => error.Value));
    }
}
