using HiringSystem.Domain.JobSeeker;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiringSystem.Infrastructure.Persistence.Configurations;

public class JobSeekerConfigurations : IEntityTypeConfiguration<JobSeeker>
{
    public void Configure(EntityTypeBuilder<JobSeeker> builder)
    {
        builder.ToTable("JobSeekers");
        builder.HasKey(j => j.Id);
        
        builder.Property(j => j.Email).IsRequired();
        builder.Property(j => j.Password).IsRequired();
        builder.Property(j => j.Name).IsRequired();
        builder.Property(j => j.Title).IsRequired();
        builder.Property(j => j.Country)
            .HasMaxLength(40);
        builder.Property(j => j.ProfilePicture)
            .HasMaxLength(100);
 
        builder.HasMany(e => e.Applications)
            .WithOne(e => e.JobSeeker)
            .HasForeignKey(e => e.JobSeekerId)
            .OnDelete(DeleteBehavior.Cascade);       
        
        builder.HasIndex(j => j.Email)
            .IsUnique();

        
        builder.Metadata.FindNavigation(nameof(JobSeeker.Applications))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}