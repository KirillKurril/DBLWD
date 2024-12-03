namespace DBLWD6.Domain.Entities
{
    public class Review : DbEntity
    {
        public string Comment { get; set; } = string.Empty;

        public int Rating { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        [ForeignKey(typeof(User), "Id")]
        public int UserId { get; set; }

        [ForeignKey(typeof(Product), "Id")]
        public int ProductId { get; set; }

        [NonMapped]
        public virtual User? User { get; set; }
        
        [NonMapped]
        public virtual Product? Product { get; set; }
    }
}
