using System.Collections.Immutable;

using HiringSystem.Domain.Job;
using HiringSystem.Domain.JobSeeker;
using HiringSystem.Domain.Talent;

using Microsoft.EntityFrameworkCore;

namespace HiringSystem.Infrastructure.Persistence;

public class HiringSystemDbContext : DbContext
{
    public HiringSystemDbContext(DbContextOptions<HiringSystemDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Application.Application?> Applications { get; set; } = null!;
    public DbSet<Job> Jobs { get; set; } = null!;
    public DbSet<Talent> Talents { get; set; } = null!;
    public DbSet<JobSeeker> JobSeekers{ get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HiringSystemDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

    }
    
}