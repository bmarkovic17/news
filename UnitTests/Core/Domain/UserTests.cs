using NewsApp.Core.Domain.UserEntity;

namespace NewsApp.UnitTests.Core.Domain;

public class UserTests
{
    [Fact]
    public void PersonalName_Create_Valid_ReturnsSuccessResult()
    {
        // Arrange

        // Act
        var result = PersonalName.Create("John", "Doe");

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Value?.Name);
        Assert.NotNull(result.Value?.Surname);
    }

    [Theory]
    [ClassData(typeof(CreateInvalidPersonalNameTestData))]
    public void PersonalName_Create_Invalid_ReturnsFailResult(string? name, string? surname)
    {
        // Arrange

        // Act
        var result = PersonalName.Create(name, surname);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotEmpty(result.Errors.SelectMany(error => error.Value));
        Assert.Null(result.Value?.Name);
        Assert.Null(result.Value?.Surname);
    }

    [Fact]
    public void Email_Create_Valid_ReturnsSuccessResult()
    {
        // Arrange

        // Act
        var result = Email.Create("user@mail");

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Value);
    }

    [Theory]
    [ClassData(typeof(CreateInvalidEmailTestData))]
    public void Email_Create_Invalid_ReturnsFailResult(string? email)
    {
        // Arrange

        // Act
        var result = Email.Create(email);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotEmpty(result.Errors.SelectMany(error => error.Value));
        Assert.Null(result.Value);
    }
}

public class CreateInvalidPersonalNameTestData : TheoryData<string?, string?>
{
    public CreateInvalidPersonalNameTestData()
    {
        Add(null, null);
        Add(null, string.Empty);
        Add(string.Empty, null);
        Add(string.Empty, string.Empty);
        Add("  ", "  ");
        Add("John", null);
        Add("John", string.Empty);
        Add("John", "   ");
        Add(null, "Doe");
        Add(string.Empty, "Doe");
        Add("   ", "Doe");
    }
}

public class CreateInvalidEmailTestData : TheoryData<string?>
{
    public CreateInvalidEmailTestData()
    {
        Add(null);
        Add(string.Empty);
        Add("   ");
        Add("user");
        Add("user@");
        Add("@mail");
        Add("@mail.com");
    }
}
