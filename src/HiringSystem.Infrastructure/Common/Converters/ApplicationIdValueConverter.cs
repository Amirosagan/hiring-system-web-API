using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HiringSystem.Infrastructure.Common.Converters;

public class ApplicationIdValueConverter : ValueConverter<Domain.Application.ValueObjects.ApplicationId, Guid>
{
    public ApplicationIdValueConverter(ConverterMappingHints mappingHints = null!) : base(
        id => id.Value,
        value => Domain.Application.ValueObjects.ApplicationId.Create(),
        mappingHints
    )
    { }
    
}