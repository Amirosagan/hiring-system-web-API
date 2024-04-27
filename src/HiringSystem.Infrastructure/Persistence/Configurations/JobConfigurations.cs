using HiringSystem.Domain.Job;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiringSystem.Infrastructure.Persistence.Configurations;

public class JobConfigurations : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        JobConfiguration(builder);
    }

    private void JobConfiguration(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("Jobs");
        
        builder.HasKey(j => j.Id);

        builder.Property(j => j.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(j => j.Details)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(j => j.TalentJobUrl)
            .HasMaxLength(200);
        

        builder.Property(j => j.JobType)
            .IsRequired();

        builder.Property(j => j.WorkPlace)
            .IsRequired();

        builder.Property(j => j.Country)
            .IsRequired()
            .HasMaxLength(20);

        builder.ComplexProperty(j => j.Salary);
        
        builder.HasMany(e => e.Applications)
            .WithOne(a=>a.Job)
            .HasForeignKey(a=>a.JobId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Talent)
            .WithMany(e => e.Jobs)
            .HasForeignKey(e => e.TalentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        
        builder.Metadata.FindNavigation(nameof(Job.Applications))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Job.Talent))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}