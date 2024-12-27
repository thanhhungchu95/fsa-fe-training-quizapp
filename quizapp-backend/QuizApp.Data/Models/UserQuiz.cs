using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Data;

public class UserQuiz
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public User? User { get; set; }

    [ForeignKey(nameof(Quiz))]
    public Guid QuizId { get; set; }

    public Quiz? Quiz { get; set; }

    public string? QuizCode { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public List<UserAnswer> UserAnswers { get; set; } = [];
}
