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
    public int Id { get; set; }

    [Required]
    public required DateTime Timestamp { get; set; }
    [Required]
    public required string Dimension { get; set; }
    [Required]
    public required double Value { get; set; }
    [Required]
    public required List<String> Tags { get; set; }
    public JsonDocument? Metadata { get; set; }
    
    public void Dispose() => Metadata?.Dispose();
}

