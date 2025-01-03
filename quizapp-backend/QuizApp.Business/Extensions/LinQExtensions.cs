using System.Linq.Expressions;

namespace QuizApp.Business;

public static class LinQExtensions
{
    public static IQueryable<T> OrderByExtensition<T>(this IQueryable<T> input, string memberName, string sort)
    {
        string methodName = $"OrderBy{(sort.ToLower() == "ascending" ? "" : "Descending")}";

        ParameterExpression parameter = Expression.Parameter(input.ElementType, "p");

        MemberExpression? memberAccess = null;
        foreach (var property in memberName.Split('.'))
        {
            memberAccess = Expression.Property(memberAccess ?? (Expression)parameter, property);
        }

        if (memberAccess == null)
        {
            throw new ArgumentNullException(nameof(memberAccess), "Member access cannot be null.");
        }
        LambdaExpression orderByLambda = Expression.Lambda(memberAccess, parameter);

        MethodCallExpression result = Expression.Call(
            typeof(Queryable),
            methodName,
            [input.ElementType, memberAccess.Type],
            input.Expression,
            Expression.Quote(orderByLambda));

        return input.Provider.CreateQuery<T>(result);
    }
}