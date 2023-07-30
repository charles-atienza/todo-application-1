namespace Exam.Database.Entities.Complementary.Interface;

public interface IEntity<TPrimaryKey>
{
    /// <summary>
    ///     Unique identifier for this entity.
    /// </summary>
    TPrimaryKey Id { get; set; }
}