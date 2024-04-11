using System.Runtime.CompilerServices;

namespace HiringSystem.Domain.Common.Models;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(ValueObject? other)
    {
        return Equals(other);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject) obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    } 
    
    public static bool operator ==(ValueObject a, ValueObject b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(ValueObject a, ValueObject b)
    {
        return !a.Equals(b);
    }
    
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }
}

public class Price : ValueObject
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
    
    public Price(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}