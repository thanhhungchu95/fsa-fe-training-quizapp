namespace QuizApp.Business
{
    public class SearchRoleQuery : SearchQuery
    {
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}