using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace QuizApp.Data;

public class Role: IdentityRole<Guid>
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Description { get; set; }

    public bool IsActive { get; set; }
}