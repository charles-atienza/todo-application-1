using Exam.Database.Complementary.Interface;

namespace Exam.Database.Complementary;

public interface IAuditedEntity<TPrimaryKey> : IEntity<TPrimaryKey>
{
    public long AddedByUserId { get; set; }
    public DateTime AddedDate { get; set; }
    public long? ModifiedByUserId { get; set; }
    public DateTime? ModifiedDate { get; set; }
}