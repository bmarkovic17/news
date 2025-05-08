using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsApp.Core.SharedKernel;

/// <summary>
/// Base class for value objects in domain-driven design.
/// Value objects are immutable and compared by their property values rather than identity.
/// </summary>
/// <typeparam name="T">The type of the value object.</typeparam>
public abstract class ValueObject<T> : IEquatable<T>
{
    /// <summary>
    /// Determines whether two specified value objects are equal.
    /// </summary>
    /// <param name="left">The first value object to compare.</param>
    /// <param name="right">The second value object to compare.</param>
    /// <returns>true if the value objects are equal; otherwise, false.</returns>
    public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right) =>
        EqualOperator(left, right);

    /// <summary>
    /// Determines whether two specified value objects are not equal.
    /// </summary>
    /// <param name="left">The first value object to compare.</param>
    /// <param name="right">The second value object to compare.</param>
    /// <returns>true if the value objects are not equal; otherwise, false.</returns>
    public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right) =>
        NotEqualOperator(left, right);

    /// <summary>
    /// Determines whether the specified object is equal to the current value object.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public virtual bool Equals(T? other) =>
        Equals((object?)other);

    /// <summary>
    /// Determines whether the specified object is equal to the current value object.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? other)
    {
        if (other is null || other.GetType() != GetType())
            return false;

        var otherValueObject = (ValueObject<T>)other;

        return GetEqualityComponents().SequenceEqual(otherValueObject.GetEqualityComponents());
    }

    /// <summary>
    /// Returns the hash code for this value object.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode() =>
        GetEqualityComponents()
            .Select(propertyValue => propertyValue?.GetHashCode() ?? 0)
            .Aggregate((result, current) => result ^ current);

    /// <summary>
    /// Gets the components of the value object to use for equality comparison.
    /// </summary>
    /// <returns>An enumerable of the equality components.</returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <summary>
    /// Determines if two value objects are equal.
    /// </summary>
    /// <param name="left">The first value object.</param>
    /// <param name="right">The second value object.</param>
    /// <returns>true if the value objects are equal; otherwise, false.</returns>
    private static bool EqualOperator(ValueObject<T>? left, ValueObject<T>? right)
    {
        if (left is null ^ right is null)
            return false;

        // At this point if one of the objects is null then we know
        // that both of them are null and we can return true
        return ReferenceEquals(left, right) || (left?.Equals(right) ?? true);
    }

    /// <summary>
    /// Determines if two value objects are not equal.
    /// </summary>
    /// <param name="left">The first value object.</param>
    /// <param name="right">The second value object.</param>
    /// <returns>true if the value objects are not equal; otherwise, false.</returns>
    private static bool NotEqualOperator(ValueObject<T>? left, ValueObject<T>? right) =>
        EqualOperator(left, right) is false;
}
