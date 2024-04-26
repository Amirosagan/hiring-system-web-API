using HiringSystem.Domain.Job;
using ErrorOr;

using HiringSystem.Application.Common.Interfaces.Persistence;
using HiringSystem.Domain.Job.ValueObjects;
using HiringSystem.Domain.Talent;
using HiringSystem.Domain.Talent.ValueObjects;

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
        var job = Job.Create(
            talent: Talent.Create("asdfasdf", "asdfasdfa", "asdfasdfasdf", "asdfasdfasd", "asdfasdfasd", "asdfasdfa"),
            talentId: TalentId.Create(),
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