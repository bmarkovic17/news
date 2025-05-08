using System.Collections.Generic;

namespace NewsApp.Core.Domain.Entities.UserEntity;

/// <summary>
/// Represents a person's name as a value object, consisting of a first name and surname.
/// </summary>
public sealed class PersonalName : ValueObject<PersonalName>
{
    private const string NameErrorKey = "name";
    private const string SurnameErrorKey = "surname";

    private PersonalName(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    public string Name { get; }

    public string Surname { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="PersonalName"/> class.
    /// </summary>
    /// <param name="name">The person's first name.</param>
    /// <param name="surname">The person's surname.</param>
    /// <returns>
    /// A result containing the created <see cref="PersonalName"/> instance if validation succeeds,
    /// or a result containing validation errors if validation fails.
    /// </returns>
    public static Result<PersonalName> Create(string? name, string? surname)
    {
        Dictionary<string, ICollection<string>> errors = [];

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(NameErrorKey, ["invalid"]);

        if (string.IsNullOrWhiteSpace(surname))
            errors.Add(SurnameErrorKey, ["invalid"]);

        var createPersonalNameResult = Result<PersonalName>.Create(
            valueFactory: () => new PersonalName(name!, surname!),
            errors);

        return createPersonalNameResult;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
        yield return Surname;
    }
}
