using HiringSystem.Domain.Job.ValueObjects;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HiringSystem.Infrastructure.Common.Converters;

public class JobIdValueConverter : ValueConverter<JobId, Guid>
{
    public JobIdValueConverter(ConverterMappingHints mappingHints = null!) : base(
        id => id.Value,
        value => JobId.Create(),
        mappingHints
    )
    { }
}