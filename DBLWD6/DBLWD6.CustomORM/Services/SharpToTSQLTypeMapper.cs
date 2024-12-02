namespace DBLWD6.CustomORM.Services
{
    internal class SharpToTSQLTypeMapper
    {
        public SharpToTSQLTypeMapper() { }
        public string GetTSQLType(Type type)
        {
            type = EnsureNotNullable(type);
            if (IsInteger(type))
                return "INT";
            if (IsString(type))
                return "VARCHAR(MAX)";
            if (IsBoolean(type))
                return "BIT";
            if (IsFloat(type))
                return "FLOAT";
            if (IsDateTime(type))
                return "DATETIME";
            if (IsDateTimeOffset(type))
                return "DATETIMEOFFSET";
            if (IsTime(type))
                return "TIME";
            throw new InvalidCastException($"Unable to convert into T-SQL data type: {type}");
        }
        Type EnsureNotNullable(Type type)
        {
            Type? underlyingType = Nullable.GetUnderlyingType(type);
            return underlyingType ?? type;
        }
        bool IsInteger(Type type)
        {
            return type == typeof(byte) ||
                   type == typeof(sbyte) ||
                   type == typeof(short) ||
                   type == typeof(ushort) ||
                   type == typeof(int) ||
                   type == typeof(uint) ||
                   type == typeof(long) ||
                   type == typeof(ulong);
        }
        
        bool IsString(Type type)
        {
            return type == typeof(string);
        }

        bool IsFloat(Type type)
        {
            return type == typeof(float) ||
                   type == typeof(double) ||
                   type == typeof(decimal);
        }
        bool IsBoolean(Type type)
        {
            return type == typeof(bool);
        }


        bool IsDateTime(Type type) 
        {
            return type == typeof(DateTime);
        }

        bool IsDateTimeOffset(Type type)
        {
            return type == typeof(DateTimeOffset);
        }

        bool IsTime(Type type)
        {
            return type == typeof(TimeSpan);
        }
    }
}
