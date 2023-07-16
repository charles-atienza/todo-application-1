#nullable disable
using Exam.Entities.Complementary;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Exam.Entities.Base;

[Index(nameof(Name), IsUnique = true)]
public class Tasks : Entity<int>
{
    [Required][StringLength(50)] public string Name { get; set; }
    [Required] public bool IsCompleted { get; set; }
}