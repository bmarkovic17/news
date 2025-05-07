using System.Collections.Generic;
using System.Net.Mail;

namespace NewsApp.Core.Domain.UserEntity;

public sealed class Email : ValueObject<Email>
{
    private const string ErrorKey = "email";

    private Email(string value) =>
        Value = value;

    public string Value { get; }

    public static Result<Email> Create(string? email)
    {
        Dictionary<string, ICollection<string>> errors = [];

        if (MailAddress.TryCreate(email, out _) is false)
            errors.Add(ErrorKey, ["invalid"]);

        var createEmailResult = Result<Email>.Create(
            valueFactory: () => new Email(email!),
            errors);

        return createEmailResult;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
