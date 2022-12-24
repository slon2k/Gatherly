namespace Gatherly.Domain.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();
    
    public override bool Equals(object? obj) => obj is ValueObject other && ValuesAreEqual(other);

    public static bool operator ==(ValueObject left, ValueObject right) => left.Equals(right);

    public static bool operator !=(ValueObject left, ValueObject right) => !left.Equals(right);

    public bool Equals(ValueObject? other) => other is not null && ValuesAreEqual(other);

    public override int GetHashCode() => GetAtomicValues().Aggregate(default(int), HashCode.Combine);

    private bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }
}
