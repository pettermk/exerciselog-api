using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExerciseLogApi.Models;

public class Persistence : DbContext
{
    public Persistence(DbContextOptions<Persistence> options) : base(options)
    {
    }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Session> Sessions { get; set; }

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

    [Timestamp]
    [Required]
    public required byte[] Timestamp { get; set; }

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