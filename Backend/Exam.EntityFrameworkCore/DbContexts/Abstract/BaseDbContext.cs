using Microsoft.EntityFrameworkCore;

namespace Exam.Database.DbContexts.Abstract;

/// <summary>
///    All dbset with default values and is shared should be placed here
/// </summary>
public abstract class BaseDbContext : DbContext
{

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
    }
}