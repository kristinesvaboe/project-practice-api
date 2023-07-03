using Microsoft.EntityFrameworkCore;
using ProjectPracticeApi.Configurations;
using ProjectPracticeApi.Entities;

public class ProjectDbContext : DbContext
{
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Epic> Epics { get; set; } = null!;
    public DbSet<ProjectTask> ProjectTasks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new EpicConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectTaskConfiguration());
    }
}