namespace DBLWD6.Domain.Entities
{
    public class Supply : DbEntity
    {
        [ForeignKey(typeof(Product), "Id")]
        public int ProductId { get; set; }

        [ForeignKey(typeof(Supplier), "Id")]
        public int SupplierId { get; set; }

        [ForeignKey(typeof(Manufacturer), "Id")]
        public int ManufacturerId { get; set; }
    }
}
