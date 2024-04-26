using HiringSystem.Domain.Common.Models;

namespace HiringSystem.Domain.JobSeeker.ValueObjects;

public sealed class JobSeekerId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }
    private JobSeekerId(Guid value)
    {
        Value = value;
    }
    
    public static JobSeekerId Create()
    {
        return new JobSeekerId(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}