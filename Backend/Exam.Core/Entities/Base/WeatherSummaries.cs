#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Exam.Entities.Complementary;
using Microsoft.EntityFrameworkCore;

namespace Exam.Entities.Base;

[Table("WeatherSummaries", Schema = "main")]
[Index(nameof(Name), IsUnique = true)]
public class WeatherSummaries : Entity<int>
{
    [Required] [StringLength(50)] public string Name { get; set; }
}