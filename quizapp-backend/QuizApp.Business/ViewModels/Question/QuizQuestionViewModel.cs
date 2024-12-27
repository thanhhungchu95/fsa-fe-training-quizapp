using QuizApp.Data;

namespace QuizApp.Business;

public class QuizQuestionViewModel
{
    public Guid Id { get; set; }
    
    public required string Content { get; set; }
    
    public QuestionType QuestionType { get; set; }

    public bool IsActive { get; set; }

    public int NumberOfAnswers { get; set; }

    public int Order { get; set; }
}