using QuizApp.Data;

namespace QuizApp.Business
{
    public class SearchQuestionQuery : SearchQuery
    {
        public string? Content { get; set; }
        public QuestionType? QuestionType { get; set; }
        public bool IsActive { get; set; }
    }
}