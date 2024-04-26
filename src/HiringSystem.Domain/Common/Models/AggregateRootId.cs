namespace HiringSystem.Domain.Common.Models;

public abstract class AggregateRootId<TId> : ValueObject
    where TId : notnull
{
    public abstract TId Value { get; protected set; }
    
}