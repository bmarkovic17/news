using System.Collections.Generic;
using System.Net.Mail;

namespace NewsApp.Core.Domain.Entities.UserEntity;

/// <summary>
/// Represents an email address as a value object.
/// </summary>
public sealed class Email : ValueObject<Email>
{
    private const string ErrorKey = "email";

    private Email(string value) =>
        Value = value;

    /// <summary>
    /// Gets the string representation of the email address.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="Email"/> class.
    /// </summary>
    /// <param name="email">The email address string to validate and encapsulate.</param>
    /// <returns>
    /// A result containing the created <see cref="Email"/> instance if validation succeeds,
    /// or a result containing validation errors if the email format is invalid.
    /// </returns>
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
