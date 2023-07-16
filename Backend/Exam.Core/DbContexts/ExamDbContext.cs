using Exam.Entities.Base;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8618

namespace Exam.DbContexts;

/// <summary>
///     The class for all DbSet of the application.
/// </summary>
public class ExamDbContext : BaseDbContext
{
    public virtual DbSet<Tasks> Tasks { get; set; }

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="options"></param>
    public ExamDbContext(DbContextOptions<ExamDbContext> options)
        : base(options)
    {
    }
}