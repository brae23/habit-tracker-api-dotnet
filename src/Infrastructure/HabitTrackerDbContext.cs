using HabitTracker.Api.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task = HabitTracker.Api.Infrastructure.Entities.Task;

namespace HabitTracker.Api.Infrastructure;

public class HabitTrackerDbContext : IdentityDbContext<User>
{
    private readonly IConfiguration _configuration;

    public HabitTrackerDbContext(
        DbContextOptions<HabitTrackerDbContext> options,
        IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connString = _configuration.GetConnectionString("PgsqlConnection");
        optionsBuilder.UseNpgsql(connString);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("habit_tracker");

        modelBuilder.Entity<User>().Property(u => u.FirstName).HasMaxLength(50);
        modelBuilder.Entity<User>().Property(u => u.LastName).HasMaxLength(50);
        modelBuilder.Entity<User>().Property(u => u.AccountCreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
    }

    public DbSet<List> Lists { get; set; }
    public DbSet<Task> Tasks { get; set; }
}