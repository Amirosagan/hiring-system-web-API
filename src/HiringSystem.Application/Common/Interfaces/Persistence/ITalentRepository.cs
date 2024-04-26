using HiringSystem.Domain.Talent;

namespace HiringSystem.Application.Common.Interfaces.Persistence;

public interface ITalentRepository
{
    void AddTalent(Talent talent);
    void UpdateTalent(Talent talent);
    void DeleteTalent(Talent talent);
    
    bool Exists(string email);
}