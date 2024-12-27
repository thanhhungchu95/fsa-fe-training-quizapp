using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Data;

public class QuizQuestion
{
    [ForeignKey(nameof(Quiz))]
    public Guid QuizId { get; set; }

    public Quiz? Quiz { get; set; }

    [ForeignKey(nameof(Question))]
    public Guid QuestionId { get; set; }

    public Question? Question { get; set; }

    public int Order { get; set; }
}
