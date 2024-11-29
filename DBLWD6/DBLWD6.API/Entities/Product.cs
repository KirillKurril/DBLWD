namespace DBLWD6.API.Entities
{
    public class Product : DbEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

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
    }
}
