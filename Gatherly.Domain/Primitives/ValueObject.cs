namespace Gatherly.Domain.Primitives;

public abstract class ValueObject
{
    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public abstract IEnumerable<object> GetAtomicValues();

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    private bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }
}
