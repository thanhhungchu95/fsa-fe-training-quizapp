using System.ComponentModel.DataAnnotations;

namespace QuizApp.Business;

public class UserEditViewModel
{
    public Guid Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public required string PhoneNumber { get; set; }

    public string? Avatar { get; set; }

    public bool IsActive { get; set; }
}
