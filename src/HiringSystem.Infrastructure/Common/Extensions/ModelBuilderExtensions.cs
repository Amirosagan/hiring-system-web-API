using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HiringSystem.Infrastructure.Common.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder UseValueConverterForId(this ModelBuilder modelBuilder, ValueConverter valueConverter)
    {
        var type = valueConverter.ModelClrType;

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entityType
                .ClrType
                .GetProperties()
                .Where(p=>p.PropertyType == type);
            
            foreach (var property in properties)
            {
                modelBuilder
                    .Entity(entityType.Name)
                    .Property(property.Name)
                    .HasConversion(valueConverter);
            }
        }

        return modelBuilder;
    }
    
}