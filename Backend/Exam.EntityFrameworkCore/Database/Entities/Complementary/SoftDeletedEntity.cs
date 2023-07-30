using Exam.Database.Entities.Complementary.Interface;

namespace Exam.Database.Complementary
{
    public abstract class SoftDeletedEntity<TPrimaryKey> : SimpleEntity<TPrimaryKey>, ISoftDeletedEntity<TPrimaryKey>
    {
        public bool IsActive { get; set; } = true;
    }
}
