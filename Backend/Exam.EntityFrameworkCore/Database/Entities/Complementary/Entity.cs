#nullable disable
using Exam.Database.Entities.Complementary.Interface;

namespace Exam.Database.Entities.Complementary;

[Serializable]
public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>, IAuditedEntity<TPrimaryKey>, ISoftDeletedEntity<TPrimaryKey>
{
    /// <summary>
    ///     Unique identifier for this entity.
    /// </summary>
    public virtual TPrimaryKey Id { get; set; }

    public long AddedByUserId { get; set; }
    public DateTime AddedDate { get; set; }
    public long? ModifiedByUserId { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }
}