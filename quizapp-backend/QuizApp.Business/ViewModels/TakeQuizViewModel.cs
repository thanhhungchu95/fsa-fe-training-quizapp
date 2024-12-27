namespace QuizApp.Business;

public class TakeQuizViewModel
{
    public Guid QuizId { get; set; }

    public Guid UserId { get; set; }

    public required string QuizCode { get; set; }
}
