namespace QuizApp.Business;

public class ChangePasswordViewModel
{
    public Guid Id { get; set; }

    public required string UserName { get; set; }

    public required string CurrentPassword { get; set; }

    public required string NewPassword { get; set; }

    public required string ConfirmPassword { get; set; }

}
