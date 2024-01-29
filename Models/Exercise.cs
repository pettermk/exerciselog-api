using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        String connString;
        var connFromEnv = System.Environment.GetEnvironmentVariable("POSTGRES_CONN_STRING");
        if (connFromEnv is not null)
        {
            connString = connFromEnv;
        }
        else
        {
            connString = @"Host=localhost;Username=postgres;Password=12345;Database=exerciselog";
        }
        optionsBuilder.UseNpgsql(connString);
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