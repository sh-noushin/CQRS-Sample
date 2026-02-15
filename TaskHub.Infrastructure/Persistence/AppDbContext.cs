using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TaskHub.Domain.Entities;
using TaskHub.Infrastructure.ReadModels;

namespace TaskHub.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
    public DbSet<TaskListRead> TaskListRead => Set<TaskListRead>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(b =>
        {
            b.ToTable("TaskItems");
            b.HasKey(x => x.Id);
            b.Property(x => x.Title).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<TaskListRead>(b =>
        {
            b.ToTable("TaskListRead");
            b.HasKey(x => x.TaskId);
            b.Property(x => x.Title).IsRequired().HasMaxLength(200);
        });
    }
}
