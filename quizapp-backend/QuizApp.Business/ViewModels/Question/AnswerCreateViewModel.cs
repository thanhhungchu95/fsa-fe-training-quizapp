namespace QuizApp.Business;

public class AnswerCreateViewModel
{
    public required string Content { get; set; }

    public bool IsCorrect { get; set; }

    public bool IsActive { get; set; }

    public Guid QuestionId { get; set; }
}