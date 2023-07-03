using Microsoft.EntityFrameworkCore;
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
}