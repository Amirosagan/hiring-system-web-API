using HiringSystem.Domain.Common.Models;

namespace HiringSystem.Domain.Talent.ValueObjects;

public sealed class TalentId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private TalentId(Guid value)
    {
        Value = value;
    }

    public static TalentId Create()
    {
        return new TalentId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}