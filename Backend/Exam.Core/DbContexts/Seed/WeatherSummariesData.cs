using Exam.Entities.Base;

namespace Exam.DbContexts.Seed;

public class WeatherSummariesData
{
     public static readonly IEnumerable<WeatherSummaries> Seed = new List<WeatherSummaries>
    {
        new() { Id = 1, Name = "Freezing" },
        new() { Id = 2, Name = "Bracing" },
        new() { Id = 3, Name = "Chilly" },
        new() { Id = 4, Name = "Cool" },
        new() { Id = 5, Name = "Mild" },
        new() { Id = 6, Name = "Warm" },
        new() { Id = 7, Name = "Balmy" },
        new() { Id = 8, Name = "Hot" },
        new() { Id = 9, Name = "Sweltering" },
        new() { Id = 10, Name = "Scorching" }
    };
}