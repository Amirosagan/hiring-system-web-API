using HiringSystem.Application.Applications.Commands.ApplyApplication;
using HiringSystem.Application.Applications.Queries.GetApplicationsWithJobId;
using HiringSystem.Contracts.Applications;

using Mapster;

using ApplicationInList = HiringSystem.Application.Applications.Queries.GetApplicationsWithJobId.ApplicationInList;

namespace HiringSystem.Api.Common.Mapping;

public class ApplicationMappingConfig :IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ApplyApplicationRequest, ApplyApplicationCommand>()
            .Map(dest => dest.JobId, src => src.JobId)
            .Map(dest => dest.Resume, src => src.Resume)
            .Map(dest => dest.JobSeekerId, src => src.JobSeekerId)
            .Map(dest => dest.Supportive, src => src.Supportive);
        
        config.NewConfig<ApplyApplicationCommandResponse, ApplyApplicationResponse>()
            .Map(dest => dest.ApplicationId, src => src.ApplicationId);
            
        
        config.NewConfig<(GetApplicationsWithJobIdRequest, string), GetApplicationsWithJobIdQuery>()
            .Map(dest => dest.TalentId, src => src.Item2)
            .Map(dest => dest.JobId, src => src.Item1.JobId)
            .Map(dest => dest.Page, src => src.Item1.Page)
            .Map(dest => dest.PageSize, src => src.Item1.PageSize);
        
        config.NewConfig<ApplicationInList, Contracts.Applications.ApplicationInList>()
            .Map(dest => dest.Id, src => src.ApplicationId)
            .Map(dest => dest.Resume, src => src.Resume)
            .Map(dest => dest.Supportive, src => src.Supportive)
            .Map(dest => dest.JobId, src => src.JobId)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt);
        
        config.NewConfig<PagedApplicationsList, GetApplicationsWithJobIdResponse>()
            .Map(dest => dest.Page, src => src.Page)
            .Map(dest => dest.PageSize, src => src.PageSize)
            .Map(dest => dest.Total, src => src.Total)
            .Map(dest => dest.HasNext, src => src.HasNext)
            .Map(dest => dest.HasPrevious, src => src.HasPrevious)
            .Map(dest => dest.Items, src => src.Items); 
            
    }
}