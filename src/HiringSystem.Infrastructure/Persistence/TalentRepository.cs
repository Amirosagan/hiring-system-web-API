using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Domain.Talent;

namespace HiringSystem.Infrastructure.Persistence;

public class TalentRepository  : ITalentRepository
{
    private readonly HiringSystemDbContext _dbContext;
    
    public TalentRepository(HiringSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void AddTalent(Talent talent)
    {
        _dbContext.Talents.Add(talent);
        _dbContext.SaveChanges();
    }

    public void UpdateTalent(Talent talent)
    {
        _dbContext.Talents.Update(talent);
        _dbContext.SaveChanges();
    }

    public void DeleteTalent(Talent talent)
    {
        _dbContext.Talents.Remove(talent);
        _dbContext.SaveChanges();
    }

    public Talent? GetTalent(string talentId)
    {
        var queryTalentId = Guid.Parse(talentId);
        return _dbContext.Talents.FirstOrDefault(t => t.Id == queryTalentId)!;
    }

    public Talent? GetTalentByEmail(string email)
    {
        return _dbContext.Talents.FirstOrDefault(t => t.Email == email);
    }

    public bool Exists(string email)
    {
        return _dbContext.Talents.Any(t => t.Email == email);
    }

    public bool ExistsWithId(string talentId)
    {
        var queryTalentId = Guid.Parse(talentId);
        return _dbContext.Talents.Any(t=>t.Id == queryTalentId);
    }
}