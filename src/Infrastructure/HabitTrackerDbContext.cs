using HabitTracker.Api.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Task = HabitTracker.Api.Infrastructure.Entities.Task;

namespace HabitTracker.Api.Infrastructure;

public class HabitTrackerDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _env;

    public HabitTrackerDbContext(
        DbContextOptions<HabitTrackerDbContext> options,
        IConfiguration configuration,
        IHostEnvironment environment)
        : base(options)
    {
        _configuration = configuration;
        _env = environment;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connString = _configuration.GetConnectionString("PgsqlConnection");
        optionsBuilder.UseNpgsql(connString);
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<List> Lists { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
}