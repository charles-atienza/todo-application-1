using Exam.Entities.Complementary;

namespace Exam.Database.Entities.Complementary
{
    public abstract class SoftDeletedEntity<TPrimaryKey> : Entity<TPrimaryKey>
    {
        public bool IsActive { get; set; } = true;
    }
}
