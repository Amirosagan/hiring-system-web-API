using HiringSystem.Domain.Common.Models;

namespace HiringSystem.Domain.Job.ValueObjects;

public sealed class JobId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private JobId(Guid value) 
    {
        Value = value;
    }
    
    public static JobId Create()
    {
        return new JobId(Guid.NewGuid());
    }
    
    public static JobId Create(Guid value)
    {
        return new JobId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}