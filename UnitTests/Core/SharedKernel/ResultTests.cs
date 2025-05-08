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
        Assert.NotNull(result.Value);
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
        Assert.Null(result.Value);
    }

    [Fact]
    public void PagedGeneric_Success_NoErrors_ReturnsSuccessResult()
    {
        // Arrange

        // Act
        var result = PagedResult<object>.Success(new object(), page: 1, size: 10);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Value);
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.Size);
    }

    [Fact]
    public void PagedGeneric_Fail_WithoutErrors_ReturnsFailResult()
    {
        // Arrange

        // Act
        var result = PagedResult<object>.Fail(new Dictionary<string, ICollection<string>>());

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotEmpty(result.Errors.SelectMany(error => error.Value));
        Assert.Null(result.Value);
        Assert.Null(result.Page);
        Assert.Null(result.Size);
    }

    [Fact]
    public void PagedGeneric_Fail_WithErrors_ReturnsFailResult()
    {
        // Arrange

        // Act
        var result = PagedResult<object>.Fail(new Dictionary<string, ICollection<string>>
        {
            [string.Empty] = ["error"]
        });

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotEmpty(result.Errors.SelectMany(error => error.Value));
        Assert.Null(result.Value);
        Assert.Null(result.Page);
        Assert.Null(result.Size);
    }

    [Fact]
    public void PagedGeneric_Create_NoErrors_ReturnsSuccessResult()
    {
        // Arrange

        // Act
        var result = PagedResult<object>.Create(
            valueFactory: () => new object(),
            page : 1,
            size: 10,
            new Dictionary<string, ICollection<string>>());

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Value);
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.Size);
    }

    [Fact]
    public void PagedGeneric_Create_WithErrors_ReturnsFailResult()
    {
        // Arrange

        // Act
        var result = PagedResult<object>.Create(
            valueFactory: () => new object(),
            page: 1,
            size: 10,
            new Dictionary<string, ICollection<string>>
            {
                [string.Empty] = ["error"]
            });

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotEmpty(result.Errors.SelectMany(error => error.Value));
        Assert.Null(result.Value);
        Assert.Null(result.Page);
        Assert.Null(result.Size);
    }
}
