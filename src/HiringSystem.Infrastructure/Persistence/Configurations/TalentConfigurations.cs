using HiringSystem.Domain.Talent;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiringSystem.Infrastructure.Persistence.Configurations;

public class TalentConfigurations : IEntityTypeConfiguration<Talent>
{
    public void Configure(EntityTypeBuilder<Talent> builder)
    {
        TalentConfiguration(builder);
    }

    private void TalentConfiguration(EntityTypeBuilder<Talent> builder)
    {
        builder.ToTable("Talents");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(t => t.WebSite)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(t => t.About)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(t => t.ProfilePicture)
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(t => t.Password)
            .HasMaxLength(1000)
            .IsRequired();

        builder.HasIndex(t => t.Email)
            .IsUnique();

        // builder.HasMany(e => e.Jobs)
        //     .WithOne(e => e.Talent)
        //     .HasForeignKey(e => e.TalentId)
        //     .OnDelete(DeleteBehavior.Cascade);

        // builder.Metadata.FindNavigation(nameof(Talent.Jobs))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}