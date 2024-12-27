using System.ComponentModel.DataAnnotations;
using QuizApp.Data;

namespace QuizApp.Business;

public class QuestionCreateViewModel
{
    [Required(ErrorMessage = "Content is required")]
    [StringLength(5000, ErrorMessage = "Content must be between 5 and 5000 characters", MinimumLength = 5)]
    public required string Content { get; set; }
    
    [Required(ErrorMessage = "QuestionType is required")]
    public QuestionType QuestionType { get; set; }

    public bool IsActive { get; set; }

    public ICollection<AnswerCreateViewModel> Answers { get; set; } = [];
}
