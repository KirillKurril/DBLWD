namespace DBLWD6.API.Entities
{
    public class Supply : DbEntity
    {
        [PrimaryKey]
        [ForeignKey(typeof(Product), "Id")]
        public int ProductId { get; set; }

        [PrimaryKey]
        [ForeignKey(typeof(Supplier), "Id")]
        public int SupplierId { get; set; }

        [PrimaryKey]
        [ForeignKey(typeof(Manufacturer), "Id")]
        public int ManufacturerId { get; set; }
    }
}
