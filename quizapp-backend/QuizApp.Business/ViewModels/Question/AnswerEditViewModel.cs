namespace QuizApp.Business;

public class AnswerEditViewModel
{
    public Guid? Id { get; set; }

    public required string Content { get; set; }

    public bool IsCorrect { get; set; }

    public bool IsActive { get; set; }
}
