using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Eventing.Reader;

using ExerciseLogApi.Models;

namespace ExerciseLogApi.Models;

public class ScoreType
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public required string Description { get; set; }

}

public class Score
{
    [ForeignKey("ScoreType")]
    public int  ScoreTypeId { get; set; }

    [Required]
    public required DateTime Timestamp { get; set; }
    [Range(1, 10)]
    public int Value { get; set; }

}
