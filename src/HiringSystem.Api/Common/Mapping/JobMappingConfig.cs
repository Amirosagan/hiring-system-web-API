using HiringSystem.Application.Jobs.Commands.AddJob;
using HiringSystem.Application.Jobs.Common;
using HiringSystem.Application.Jobs.Queries.GetJobDetails;
using HiringSystem.Application.Jobs.Queries.GetJobs;
using HiringSystem.Application.Jobs.Queries.GetJobsWithTalentId;
using HiringSystem.Contracts.Jobs;
using HiringSystem.Domain.Job;

using Mapster;

using GetJobsResponse = HiringSystem.Contracts.Jobs.GetJobsResponse;

namespace HiringSystem.Api.Common.Mapping;

public class JobMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(AddJobRequest request, string talentId), AddJobCommand>()
            .Map(dest => dest.TalentId, src => src.talentId)
            .Map(dest => dest, src => src.request);
        
        config.NewConfig<Job, AddJobResponse>()
            .Map(dest => dest.jobId, src => src.Id.ToString());
        
        MapJobsRequest(config);
        MapJobsResponse(config);
        MapGetJobRequest(config);
        MapGetJobResponse(config);
        MapGetJobsWithTalentIdRequest(config);
    }
    
    private void MapJobsRequest(TypeAdapterConfig config)
    {
        config.NewConfig<GetJobsRequest, GetJobsQuery>()
            .Map(dest => dest, src => src);
    }
    
    private void MapJobsResponse(TypeAdapterConfig config)
    {
        config.NewConfig<PagedJobList, GetJobsResponse>()
            .Map(dest => dest.Total, src => src.TotalCount)
            .Map(dest => dest.Page, src => src.Page)
            .Map(dest=>dest.HasNext, src => src.HasNextPage)
            .Map(dest=>dest.HasPrevious, src => src.HasPreviousPage);
    }
    
    private void MapGetJobRequest(TypeAdapterConfig config)
    {
        config.NewConfig<GetJobRequest, GetJobDetailsQuery>()
            .Map(dest=>dest.JobId, src=>src.JobId);
        
    }
    
    private void MapGetJobResponse(TypeAdapterConfig config)
    {
        config.NewConfig<Job, GetJobResponse>()
            .Map(dest => dest.JobId, src => src.Id.ToString())
            .Map(dest => dest.TalentId, src => src.TalentId.ToString())
            .Map(dest => dest.Salary, src => src.Salary)
            .Map(dest => dest.WorkPlace, src => src.WorkPlace)
            .Map(dest => dest.JobType, src => src.JobType)
            .Map(dest => dest.TalentJobUrl, src => src.TalentJobUrl)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt);
    }
    
    private void MapSalary(TypeAdapterConfig config)
    {
        config.NewConfig<Salary, Domain.Job.ValueObjects.Salary>()
            .Map(dest => dest.Maximum, src => src.Max)
            .Map(dest => dest.Minimum, src => src.Min)
            .Map(dest => dest.Currency, src => src.Currency);
    }
    
    private void MapGetJobsWithTalentIdRequest(TypeAdapterConfig config)
    {
        config.NewConfig<(GetJobsWithTalentIdRequest request, string talentId), GetJobsWithTalentIdQuery>()
            .Map(dest => dest.TalentId, src => src.talentId)
            .Map(dest => dest, src => src.request);
    }
}