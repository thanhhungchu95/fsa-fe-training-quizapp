using System.ComponentModel.DataAnnotations;

namespace QuizApp.Business;

public class QuizEditViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(255, ErrorMessage = "Title must be between 5 and 255 characters", MinimumLength = 5)]
    public required string Title { get; set; }

    public string? Description { get; set; }

    [Required(ErrorMessage = "Duration is required")]
    [Range(1, 3600, ErrorMessage = "Duration must be between 1 and 3600 seconds")]
    public int Duration { get; set; }

    [StringLength(255, ErrorMessage = "Thumbnail URL must be between 5 and 255 characters", MinimumLength = 5)]
    public string? ThumbnailUrl { get; set; }

    public bool IsActive { get; set; }

    public ICollection<QuestionIdWithOrderViewModel> QuestionIdWithOrders { get; set; } = [];
}
