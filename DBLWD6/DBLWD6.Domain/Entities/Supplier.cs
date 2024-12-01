namespace DBLWD6.Domain.Entities
{
    public class Supplier : DbEntity
    {
        [NonNull]
        public string Name { get; set; }

        [NonNull]
        public string Address { get; set; }

        [NonNull]
        public string Phone { get; set; }
    }
}
