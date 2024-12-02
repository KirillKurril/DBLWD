namespace DBLWD6.Domain.Entities
{
    public class Product : DbEntity
    {
        [NonNull]
        public string Name { get; set; }

        [NonNull]
        public string ArticleNumber { get; set; }

        [NonNull]
        public string Description { get; set; }

        public decimal PricePerUnit { get; set; } = 9.99M;

        public string? Image { get; set; }

        public int Count { get; set; } = 5;

        [NonNull]
        [ForeignKey(typeof(Category), "Id")]
        public int CategoryId { get; set; }

        [NonMapped]
        public Category? Category { get; set; }
        
        [NonMapped]
        public List<Supplier>? Suppliers { get; set; }

        [NonMapped]
        public List<Manufacturer>? Manufacturers { get; set; }

        [NonMapped]
        public List<PickupPoint>? PickupPoints { get; set; }
    }
}
