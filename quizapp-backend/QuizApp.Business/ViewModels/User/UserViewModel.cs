namespace QuizApp.Business;

public class UserViewModel
{
    public Guid Id { get; set; }

    public required string FirstName { get; set; }
   
    public required string LastName { get; set; }

    public string DisplayName => $"{FirstName} {LastName}";
    
    public required string UserName { get; set; }
   
    public required string Email { get; set; }

    public required string PhoneNumber { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? Avatar { get; set; }

    public bool IsActive { get; set; }

    public List<string> Roles { get; set; } = [];
}