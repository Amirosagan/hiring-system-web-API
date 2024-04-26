using HiringSystem.Domain.Common.Enums;
using HiringSystem.Domain.Common.Models;

namespace HiringSystem.Domain.Job.ValueObjects;

public sealed class Salary : ValueObject
{
    public decimal Minimum { get; private set; }
    public decimal Maximum { get; private set; }
    public string Currency { get; private set; }
    public Period Period { get; private set; }
    
    private Salary(decimal minimum, decimal maximum, string currency, Period period)
    {
        Minimum = minimum;
        Maximum = maximum;
        Currency = currency;
        Period = period;
    }
    
    public static Salary Create(decimal minimum, decimal maximum, string currency, Period period)
    {
        return new Salary(minimum, maximum, currency, period);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Minimum;
        yield return Maximum;
        yield return Currency;
        yield return Period;
    }
#pragma warning disable CS8618
    private Salary()
    {
        
    }
#pragma warning restore CS8618
}