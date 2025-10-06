using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json;

using ExerciseLogApi.Models;

namespace ExerciseLogApi.Models;

public class Timeseries: IDisposable
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("date_time")]
    public required DateTime Timestamp { get; set; }
    [Required]
    [Column("dimension")]
    public required string Dimension { get; set; }
    [Required]
    [Column("value")]
    public required double Value { get; set; }
    [Required]
    [Column("tags")]
    public required List<String> Tags { get; set; }
    [Column("metadata")]
    public JsonDocument? Metadata { get; set; }
    
    public void Dispose() => Metadata?.Dispose();
}

