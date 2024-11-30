namespace DBLWD6.Domain.Entities
{
    public class Supplier : DbEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        [NonNull]
        public string Name { get; set; }

        [NonNull]
        public string Address { get; set; }

        [NonNull]
        public string Phone { get; set; }
    }
}
