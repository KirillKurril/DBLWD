namespace DBLWD6.CustomORM.Model
{
    public class SQLEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Query { get; set; } = string.Empty;
        public SQLEntity() { }
        public SQLEntity(string name, string query)
        {
            Name = name;
            Query = query;
        }
    }
}
