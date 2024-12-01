namespace DBLWD6.Domain.Entities
{
    public class FAQ : DbEntity
    {
        [NonNull]
        [ForeignKey(typeof(Article), "Id")]
        public int ArticleId { get; set; }

        [NonMapped]
        public Article Article { get; set; }

        [NonNull]
        public string Question { get; set; }
    }
}
