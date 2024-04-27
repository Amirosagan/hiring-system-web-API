using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiringSystem.Infrastructure.Persistence.Configurations;

public class ApplicationConfigurations : IEntityTypeConfiguration<Domain.Application.Application>
{
    public void Configure(EntityTypeBuilder<Domain.Application.Application> builder)
    {
        ApplicationConfiguration(builder);
    }

    private void ApplicationConfiguration(EntityTypeBuilder<Domain.Application.Application> builder)
    {
        builder.ToTable("Applications");
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Resume)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(a => a.Supportive)
            .HasMaxLength(5000);
        
        
    }
}