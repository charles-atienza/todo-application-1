#nullable disable

using Exam.Database.Entities.Complementary;
using System.ComponentModel.DataAnnotations;

namespace Exam.Database.Complementary;

[Serializable]
public abstract class AuditedEntity<TPrimaryKey> : SimpleEntity<TPrimaryKey>, IAuditedEntity<TPrimaryKey>
{
    [Required]
    public long AddedByUserId { get; set; }
    [Required]
    public DateTime AddedDate { get; set; }

    public long? ModifiedByUserId { get; set; }
    public DateTime? ModifiedDate { get; set; }
}