using HiringSystem.Domain.Job;
using ErrorOr;

using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Domain.Common.Errors;
using HiringSystem.Domain.Job.ValueObjects;

using MediatR;

namespace HiringSystem.Application.Jobs.Commands.AddJob;

public class AddJobCommandHandler : IRequestHandler<AddJobCommand, ErrorOr<Job>>
{
    private readonly IJobRepository _jobRepository;
    private readonly ITalentRepository _talentRepository;
    
    public AddJobCommandHandler(IJobRepository jobRepository, ITalentRepository talentRepository)
    {
        _jobRepository = jobRepository;
        _talentRepository = talentRepository;
    }
    
    public Task<ErrorOr<Job>> Handle(AddJobCommand request, CancellationToken cancellationToken)
    {
        var talent = _talentRepository.GetTalent(request.TalentId);
        
        if (ReferenceEquals(talent, null))
        {
            return Task.FromResult<ErrorOr<Job>>(Errors.Job.TalentNotFound(request.TalentId));
        }

        var job = Job.Create(
            talent: talent,
            talentId: talent.Id,
            title: request.Title,
            details: request.Description,
            jobType: request.JobType,
            workPlace: request.WorkPlace,
            country: request.Country,
            salary: Salary.Create(request.Salary.Min, request.Salary.Max, request.Salary.Currency, request.Salary.Period),
            talentJobUrl: request.TalentJobUrl
        );
        
        _jobRepository.AddJob(job);

        return Task.FromResult<ErrorOr<Job>>(job);
    }
}