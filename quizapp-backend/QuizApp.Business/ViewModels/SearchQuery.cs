namespace QuizApp.Business;

public class SearchQuery
{
    public int Page { get; set; }

    public int Size { get; set; }

    public string? OrderBy { get; set; }

    public OrderDirection OrderDirection { get; set; }
}

public enum OrderDirection
{
    Ascending = 0,
    Descending = 1
}
