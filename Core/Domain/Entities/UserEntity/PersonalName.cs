using System.Collections.Generic;

namespace NewsApp.Core.Domain.Entities.UserEntity;

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
