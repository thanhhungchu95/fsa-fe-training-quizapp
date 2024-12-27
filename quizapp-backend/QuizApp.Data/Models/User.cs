using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace QuizApp.Data;

public class User : IdentityUser<Guid>
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string FirstName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string LastName { get; set; }

    [NotMapped]
    public string DisplayName => $"{FirstName} {LastName}";

    [MaxLength(500)]
    public string? Avatar { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public bool IsActive { get; set; }

    public ICollection<UserQuiz> UserQuizzes { get; set; } = [];
}