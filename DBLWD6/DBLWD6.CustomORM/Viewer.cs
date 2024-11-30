using System;
using System.Linq.Expressions;

public class Sample
{
    public int Age { get; set; }
    public string Name { get; set; }
}

class Program
{
    static void Main()
    {
        Expression<Func<Sample, bool>> predicate = x => x.Age > 18 && x.Name == "John";

        var sqlWhereCondition = ConvertToSqlWhereCondition(predicate);
        Console.WriteLine(sqlWhereCondition);  // Output: "Age > 18 AND Name = 'John'"
    }

    public static string ConvertToSqlWhereCondition(Expression<Func<Sample, bool>> predicate)
    {
        if (predicate.Body is BinaryExpression binaryExpression)
        {
            return ParseBinaryExpression(binaryExpression);
        }
        else
        {
            throw new InvalidOperationException("Unsupported expression type.");
        }
    }

    public static string ParseBinaryExpression(BinaryExpression binaryExpression)
    {
        var left = ParseExpression(binaryExpression.Left);
        var right = ParseExpression(binaryExpression.Right);

        string operatorSymbol = GetSqlOperator(binaryExpression.NodeType);

        return $"{left} {operatorSymbol} {right}";
    }

    public static string ParseExpression(Expression expression)
    {
        switch (expression)
        {
            case MemberExpression member:
                return member.Member.Name;
            case ConstantExpression constant:
                return constant.Value is string ? $"'{constant.Value}'" : constant.Value.ToString();
            default:
                throw new InvalidOperationException("Unsupported expression.");
        }
    }

    public static string GetSqlOperator(ExpressionType nodeType)
    {
        return nodeType switch
        {
            ExpressionType.Equal => "=",
            ExpressionType.NotEqual => "!=",
            ExpressionType.GreaterThanOrEqual => ">=",
            ExpressionType.LessThanOrEqual => "<=",
            ExpressionType.GreaterThan => ">",
            ExpressionType.LessThan => "<",
            ExpressionType.AndAlso => "AND",
            ExpressionType.OrElse => "OR",
            _ => throw new InvalidOperationException($"Unsupported operator {nodeType}")
        };
    }
}
