using Exam.Database.Complementary;
using Exam.Database.Complementary.Interface;

namespace Exam.Database.Entities.Complementary
{
    public abstract class SoftDeletedEntity<TPrimaryKey> : SimpleEntity<TPrimaryKey>, ISoftDeletedEntity<TPrimaryKey>
    {
        public bool IsActive { get; set; } = true;
    }
}
