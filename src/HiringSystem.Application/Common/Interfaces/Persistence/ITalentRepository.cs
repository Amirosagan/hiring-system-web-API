using HiringSystem.Domain.Talent;

namespace HiringSystem.Application.Common.Interfaces.Persistence;

public interface ITalentRepository
{
    void AddTalent(Talent talent);
    void UpdateTalent(Talent talent);
    void DeleteTalent(Talent talent);
    Talent? GetTalent(string talentId);
    Talent? GetTalentByEmail(string email);
    
    bool Exists(string email);
    bool ExistsWithId(string talentId);
    
}