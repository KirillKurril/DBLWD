namespace DBLWD6.Domain.Entities
{
    public class ProductPickupPoint : DbEntity
    {
        [ForeignKey(typeof(PickupPoint), "Id")]
        public int PickupPointId { get; set; }

        [ForeignKey(typeof(Product), "Id")]
        public int ProductId { get; set; }
    }
}
