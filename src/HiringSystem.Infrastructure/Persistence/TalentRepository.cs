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
    
    public bool Exists(string email)
    {
        return _dbContext.Talents.Any(t => t.Email == email);
    }
}