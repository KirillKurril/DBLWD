namespace DBLWD6.Domain.Entities
{
    public class ProductPickupPoint : DbEntity
    {
        [PrimaryKey]
        [ForeignKey(typeof(PickupPoint), "Id")]
        public int PickupPointId { get; set; }

        [PrimaryKey]
        [ForeignKey(typeof(Product), "Id")]
        public int ProductId { get; set; }
    }
}
