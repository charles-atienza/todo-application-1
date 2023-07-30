using Exam.Database.DbContexts.Abstract;
using Exam.Database.Base;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8618

namespace Exam.Database.DbContexts;

/// <summary>
///     The class for all DbSet of the application.
/// </summary>
public class ExamDbContext : BaseDbContext
{
    public virtual DbSet<Base.Task> Tasks { get; set; }

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="options"></param>
    public ExamDbContext(DbContextOptions<ExamDbContext> options)
        : base(options)
    {
    }
}