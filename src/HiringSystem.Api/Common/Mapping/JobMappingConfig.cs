using HiringSystem.Application.Common.Helper;
using HiringSystem.Application.Jobs.Commands.AddJob;
using HiringSystem.Application.Jobs.Queries.GetJobs;
using HiringSystem.Contracts.Jobs;
using HiringSystem.Domain.Job;

using Mapster;

using GetJobsResponse = HiringSystem.Contracts.Jobs.GetJobsResponse;
using JobInList = HiringSystem.Contracts.Jobs.JobInList;

namespace HiringSystem.Api.Common.Mapping;

public class JobMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(AddJobRequest request, string talentId), AddJobCommand>()
            .Map(dest => dest.TalentId, src => src.talentId)
            .Map(dest => dest, src => src.request);
        
        config.NewConfig<Job, AddJobResponse>()
            .Map(dest => dest.jobId, src => src.Id.Value.ToString());
        
        MapJobsRequest(config);
        MapJobsResponse(config);
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
}