namespace QuizApp.Business;

public class QuizViewModel
{
    public Guid Id { get; set; }
    
    public required string Title { get; set; }
    
    public string? Description { get; set; }

    public int Duration { get; set; }

    public string? ThumbnailUrl { get; set; }

    public bool IsActive { get; set; }

    public int NumberOfQuestions { get; set; }
}
