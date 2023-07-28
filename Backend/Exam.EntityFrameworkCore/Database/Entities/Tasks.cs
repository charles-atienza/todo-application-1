﻿#nullable disable
using Exam.Database.Entities.Complementary;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Exam.Database.Base;

[Index(nameof(Name), IsUnique = true)]
public class Tasks : SoftDeletedEntity<int>
{
    [Required][StringLength(50)] public string Name { get; set; }
    [Required] public bool IsCompleted { get; set; }
}