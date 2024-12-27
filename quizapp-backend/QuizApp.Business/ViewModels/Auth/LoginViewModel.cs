using QuizApp.Data;

namespace QuizApp.Business;

public class LoginViewModel
{
    public required string UserName { get; set; }

    public required string Password { get; set; }
}
