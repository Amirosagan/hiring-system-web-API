namespace HiringSystem.Domain.Common.Models;

public class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    protected Entity(TId id)
    {
        Id = id; 
    }

    private TId Id { get; set; } 
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Entity<TId>)obj);
    }
    
    public static bool operator ==(Entity<TId> a, Entity<TId> b)
    {
        return a.Equals(b);
    }
    
    public static bool operator !=(Entity<TId> a, Entity<TId> b)
    {
        return !a.Equals(b);
    }

    public bool Equals(Entity<TId>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    protected Entity()
    { }
}
