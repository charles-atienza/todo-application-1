using Exam.DbContexts.Seed;
using Exam.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Exam.DbContexts;

/// <summary>
///    All dbset with default values and is shared should be placed here
/// </summary>
public abstract class BaseDbContext : DbContext
{
    public virtual DbSet<WeatherSummaries> WeatherSummaries { get; set; }
    
    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="options"></param>
    protected BaseDbContext(DbContextOptions options)
        : base(options)
    {
    }
    
    /// <summary>
        ///     Add additional configurations on model creation
        ///     Please Consider using Attributes before using model building
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(ExamConstants.DefaultSchemaName);

            modelBuilder.Entity<WeatherSummaries>().HasData(WeatherSummariesData.Seed);
        }
}