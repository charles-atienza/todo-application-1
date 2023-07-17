#nullable disable
using Exam.Entities.Complementary.Interface;

namespace Exam.Entities.Complementary;

[Serializable]
public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
{
    /// <summary>
    ///     Unique identifier for this entity.
    /// </summary>
    public virtual TPrimaryKey Id { get; set; }
}