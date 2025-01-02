namespace QuizApp.Business
{
    public class SearchUserQuery : SearchQuery
    {
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}