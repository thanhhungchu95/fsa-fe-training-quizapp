namespace QuizApp.Business;

public class QuizForTestViewModel
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public string? QuizCode { get; set; }

    public int Duration { get; set; }

    public DateTime StartTime { get; set; }

    public List<QuestionForTestViewModel> Questions { get; set; } = [];
}
