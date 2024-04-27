using HiringSystem.Application.Applications.Commands.ApplyApplication;
using HiringSystem.Contracts.Applications;

using Mapster;

namespace HiringSystem.Api.Common.Mapping;

public class ApplicationMappingConfig :IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(ApplyApplicationRequest, string JobSeekerId), ApplyApplicationCommand>()
            .Map(dest => dest.JobSeekerId, src => src.JobSeekerId)
            .Map(dest => dest, src => src.Item1);
        
        
        
    }
}