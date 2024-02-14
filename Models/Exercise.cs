using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Eventing.Reader;

namespace ExerciseLogApi.Models;

public class Persistence : DbContext
{
    public Persistence(DbContextOptions<Persistence> options) : base(options)
    {
    }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Set> Sets { get; set; }

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


public class Exercise
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public string Description { get; set; }
}


public class Set
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Exercise")]
    public int ExerciseId { get; set; }

    [Required]
    public required DateTime Timestamp { get; set; }

    public float Weight { get; set; }
    public int Reps { get; set; }

    // Add other properties for the Set entity
}

public class Session
{
    [Key]
    public int Id {get; set; }
    [ForeignKey("Exercise")]
    public int ExerciseId { get; set; }

    [Required]
    public required DateTime Timestamp { get; set; }

    [Required]
    public TimeSpan Duration { get; set; }
}