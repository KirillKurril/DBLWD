namespace DBLWD6.API.Entities
{
    public class FAQ : DbEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        [NonNull]
        [ForeignKey(typeof(Article), "Id")]
        public int ArticleId { get; set; }

        [NonNull]
        public string Question { get; set; }
    }
}
