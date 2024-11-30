namespace DBLWD6.Domain.Entities
{
    public class PickupPoint : DbEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        [NonNull]
        public string Name { get; set; }

        [NonNull]
        public string Address { get; set; }

        [NonNull]
        public string PhoneNumber { get; set; }
    }
}
