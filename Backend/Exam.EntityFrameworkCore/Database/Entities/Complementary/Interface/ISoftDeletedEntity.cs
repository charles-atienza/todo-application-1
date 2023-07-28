namespace Exam.Database.Complementary.Interface
{
    public interface ISoftDeletedEntity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public bool IsActive { get; set; }
    }
}
