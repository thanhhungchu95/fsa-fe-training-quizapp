namespace QuizApp.Business
{
    public class SearchQuizQuery : SearchQuery
    {
        public string? Title { get; set; }
        public bool IsActive { get; set; }
    }
}