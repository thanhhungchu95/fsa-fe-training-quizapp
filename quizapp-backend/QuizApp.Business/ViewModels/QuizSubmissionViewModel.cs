namespace QuizApp.Business;

public class QuizSubmissionViewModel
{
    public Guid QuizId { get; set; }

    public Guid UserId { get; set; }

    public string? QuizCode { get; set; }

    public List<UserAnswerSubmissionViewModel> UserAnswers { get; set; } = [];
}
