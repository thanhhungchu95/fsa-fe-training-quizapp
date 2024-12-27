using System.ComponentModel.DataAnnotations;

namespace QuizApp.Business;

public class UserCreateViewModel
{
    [Required(ErrorMessage = "First Name is required")]
    [StringLength(50, ErrorMessage = "First Name must be between 3 and 50 characters", MinimumLength = 3)]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    [StringLength(50, ErrorMessage = "Last Name must be between 3 and 50 characters", MinimumLength = 3)]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters", MinimumLength = 3)]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(50, ErrorMessage = "Password must be between 8 and 50 characters", MinimumLength = 8)]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
    public required string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Date of Birth is required")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Phone Number is required")]
    [StringLength(50, ErrorMessage = "Phone Number must be between 3 and 50 characters", MinimumLength = 3)]
    public required string PhoneNumber { get; set; }

    [StringLength(500, ErrorMessage = "Avatar URL must be between 5 and 500 characters", MinimumLength = 5)]
    public string? Avatar { get; set; }

    public bool IsActive { get; set; }
}
