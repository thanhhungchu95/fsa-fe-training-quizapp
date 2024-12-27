using QuizApp.Data;

namespace QuizApp.Business;

public class QuestionForTestViewModel
{
    public Guid Id { get; set; }

    public required string Content { get; set; }

    public QuestionType QuestionType { get; set; }

    public List<AnswerForTestViewModel> Answers { get; set; } = [];
}
