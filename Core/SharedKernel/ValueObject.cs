using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsApp.Core.SharedKernel;

public abstract class ValueObject<T> : IEquatable<T>
{
    public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right) =>
        EqualOperator(left, right);

    public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right) =>
        NotEqualOperator(left, right);

    public virtual bool Equals(T? other) =>
        Equals((object?)other);

    public override bool Equals(object? other)
    {
        if (other is null || other.GetType() != GetType())
            return false;

        var otherValueObject = (ValueObject<T>)other;

        return GetEqualityComponents().SequenceEqual(otherValueObject.GetEqualityComponents());
    }

    public override int GetHashCode() =>
        GetEqualityComponents()
            .Select(propertyValue => propertyValue?.GetHashCode() ?? 0)
            .Aggregate((result, current) => result ^ current);

    protected abstract IEnumerable<object?> GetEqualityComponents();

    private static bool EqualOperator(ValueObject<T>? left, ValueObject<T>? right)
    {
        if (left is null ^ right is null)
            return false;

        // At this point if one of the objects is null then we know
        // that both of them are null and we can return true
        return ReferenceEquals(left, right) || (left?.Equals(right) ?? true);
    }

    private static bool NotEqualOperator(ValueObject<T>? left, ValueObject<T>? right) =>
        EqualOperator(left, right) is false;
}
