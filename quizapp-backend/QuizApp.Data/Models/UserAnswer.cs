using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Data;

public class UserAnswer
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(UserQuiz))]
    public Guid UserQuizId { get; set; }

    public UserQuiz? UserQuiz { get; set; }

    [ForeignKey(nameof(Question))]
    public Guid QuestionId { get; set; }

    public Question? Question { get; set; }

    [ForeignKey(nameof(Answer))]
    public Guid AnswerId { get; set; }

    public Answer? Answer { get; set; }

    public bool IsCorrect { get; set; }
}