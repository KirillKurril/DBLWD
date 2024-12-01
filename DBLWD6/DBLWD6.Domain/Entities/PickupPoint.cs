namespace DBLWD6.Domain.Entities
{
    public class PickupPoint : DbEntity
    {
        [NonNull]
        public string Name { get; set; }

        [NonNull]
        public string Address { get; set; }

        [NonNull]
        public string PhoneNumber { get; set; }
    }
}
