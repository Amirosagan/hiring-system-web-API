using System.ComponentModel.DataAnnotations.Schema;

namespace HiringSystem.Domain.Common.Models;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId: notnull
{
    // [Column(TypeName = "char(36)")]
    public TId Id { get; protected set; }

    protected AggregateRoot(TId id) 
    {
        Id = id;
    }
    
    protected AggregateRoot()
    {}
    
}