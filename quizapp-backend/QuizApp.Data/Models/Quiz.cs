using System.ComponentModel.DataAnnotations;

namespace QuizApp.Data;

public class Quiz
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 5)]
    public required string Title { get; set; }
    
    public string? Description { get; set; }
    
    [Required]
    [Range(1, 3600)]
    public int Duration { get; set; }

    [MaxLength(500)]
    public string? ThumbnailUrl { get; set; }

    public bool IsActive { get; set; }

    public ICollection<UserQuiz> UserQuizzes { get; set; } = [];

    public ICollection<QuizQuestion> QuizQuestions { get; set; } = [];
}
