using QuizApp.Data;

namespace QuizApp.Business;

public class QuestionViewModel
{
    public Guid Id { get; set; }
    
    public required string Content { get; set; }
    
    public QuestionType QuestionType { get; set; }

    public bool IsActive { get; set; }

    public int NumberOfAnswers { get; set; }
}

public class AnswerViewModel
{
    public Guid Id { get; set; }

    public required string Content { get; set; }

    public bool IsCorrect { get; set; }

    public bool IsActive { get; set; }

    public Guid QuestionId { get; set; }
}