using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Data;

public class Answer
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(5000, MinimumLength = 5)]
    public required string Content { get; set; }

    [Required]
    public bool IsCorrect { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey(nameof(Question))]
    public Guid QuestionId { get; set; }

    public Question? Question { get; set; }
}
