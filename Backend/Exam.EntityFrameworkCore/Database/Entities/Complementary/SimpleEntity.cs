#nullable disable
using Exam.Database.Complementary.Interface;

namespace Exam.Database.Complementary;

[Serializable]
public abstract class SimpleEntity<TPrimaryKey> : IEntity<TPrimaryKey>
{
    /// <summary>
    ///     Unique identifier for this entity.
    /// </summary>
    public virtual TPrimaryKey Id { get; set; }
}