using Microsoft.EntityFrameworkCore;

namespace ExerciseLogApi.Models;

public class Persistence : DbContext
{
    public Persistence(DbContextOptions<Persistence> options) : base(options)
    {
    }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Set> Sets { get; set; }
    public DbSet<Timeseries> Timeseries { get; set; }

    private string ConnectionStringFromEnv() {
        var configBuilder = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();
        string? host = configBuilder["PG_HOST"];
        string? database = configBuilder["PG_DATABASE"];
        string? user = configBuilder["PG_USER"];
        string? password = configBuilder["PG_PASSWORD"];

        if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(database) ||
               string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
        {
            return @"Host=localhost;Username=postgres;Password=12345;Database=exerciselog";
        }

        return $"Host={host};Username={user};Password={password};Database={database}";
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connFromEnv = ConnectionStringFromEnv();
        optionsBuilder.UseNpgsql(connFromEnv);
    }
}
