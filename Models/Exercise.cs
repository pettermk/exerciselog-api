using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Eventing.Reader;

namespace ExerciseLogApi.Models;



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