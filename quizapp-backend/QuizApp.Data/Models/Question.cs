using System.ComponentModel.DataAnnotations;

namespace QuizApp.Data;

public class Question
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(5000, MinimumLength = 5)]
    public required string Content { get; set; }

    [Required]
    public QuestionType QuestionType { get; set; }

    public bool IsActive { get; set; }

    public ICollection<QuizQuestion> QuizQuestions { get; set; } = [];

    public ICollection<Answer> Answers { get; set; } = [];
}