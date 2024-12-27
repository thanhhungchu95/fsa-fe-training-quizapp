using System.ComponentModel.DataAnnotations;

namespace QuizApp.Business;

public class RoleEditViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name must be between 3 and 50 characters", MinimumLength = 3)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(50, ErrorMessage = "Description must be between 3 and 50 characters", MinimumLength = 3)]
    public required string Description { get; set; }

    public bool IsActive { get; set; }
}
