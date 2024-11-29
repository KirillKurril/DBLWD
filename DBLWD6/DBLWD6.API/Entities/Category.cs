namespace DBLWD6.API.Entities
{
    public class Category : DbEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        [NonNull]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
