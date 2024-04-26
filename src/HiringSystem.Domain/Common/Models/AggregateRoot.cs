using System.ComponentModel.DataAnnotations.Schema;

namespace HiringSystem.Domain.Common.Models;

public abstract class AggregateRoot<TId, TIdType> : Entity<TId>
    where TId : AggregateRootId<TIdType>
    where TIdType : notnull
{
    // [Column(TypeName = "char(36)")]
    public new AggregateRootId<TIdType> Id { get; protected set; }

    protected AggregateRoot(TId id) 
    {
        Id = id;
    }
    
    protected AggregateRoot()
    {}
    
}