using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPracticeApi.Entities;

namespace ProjectPracticeApi.Configurations;

public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.HasKey(pt => pt.ID);

        builder.Property(pt => pt.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(pt => pt.Description)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(pt => pt.Responsible)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(pt => pt.Priority)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(pt => pt.Epic)
            .WithMany(e => e.ProjectTasks)
            .HasForeignKey(pt => pt.EpicID)
            .IsRequired();
    }
}