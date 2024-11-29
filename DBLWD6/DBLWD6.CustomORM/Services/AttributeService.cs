using DBLWD6.CustomORM.Attributes;
using System.Text;

namespace DBLWD6.CustomORM.Services
{
    internal static class AttributeService
    {
        public static bool IsForeignKey(object attribute)
        {
            return attribute is ForeignKey;
        }

        public static string GetForeignKeyExpression(object attribute, string propertyName)
        {
            if(attribute is ForeignKey foreignKeyAttribute)
            {
                var builder = new StringBuilder();
                builder.Append("FOREIGN KEY (").Append(propertyName).Append(") ")
                       .Append("REFERENCES [").Append(foreignKeyAttribute.TargetTable).Append("] ")
                       .Append("(").Append(foreignKeyAttribute.TargetProperty).Append(") ")
                       .Append("ON DELETE ").Append(foreignKeyAttribute.OnDeleteAction);
                return builder.ToString();

            }
            else
                return string.Empty;
        }

        public static string GetConstraintAsString(object attribute)
        {
            if (attribute is Identity identityAttribute)
            {
                var builder = new StringBuilder();
                builder.Append("IDENTITY(")
                       .Append(identityAttribute.Seed).Append(",")
                       .Append(identityAttribute.Increment).Append(")");
                return builder.ToString();
            }

            if (attribute is NonNull nonNullAttribute)
            {
                return "NOT NULL";
            }

            if (attribute is PrimaryKey primaryKeyAttribute)
            {
                return "PRIMARY KEY";
            }

            if (attribute is Unique uniqueAttribute)
            {
                return "UNIQUE";
            }

            throw new ArgumentException("Unsupported attribute type", nameof(attribute));
        }
    }
}
