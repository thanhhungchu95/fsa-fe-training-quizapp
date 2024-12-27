namespace QuizApp.Business;

public class RegisterViewModel
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string UserName { get; set; }

    public required string Password { get; set; }

    public required string ConfirmPassword { get; set; }

    public DateTime DateOfBirth { get; set; }

    public required string PhoneNumber { get; set; }

    public bool IsActive { get; set; }
}
