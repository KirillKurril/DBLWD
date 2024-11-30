namespace DBLWD6.Domain.Entities
{
    public class Category : DbEntity
    {
        [NonNull]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
