using ErrorOr;

using HiringSystem.Domain.Common.Models;

namespace HiringSystem.Domain.Application.ValueObjects;

public sealed class ApplicationId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }
    
    private ApplicationId(Guid value)
    {
        Value = value;
    }
    
    public static ApplicationId Create(Guid value)
    {
        return new ApplicationId(value);
    }
    
    public static ApplicationId Create()
    {
        return new ApplicationId(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    private ApplicationId()
    {}
}