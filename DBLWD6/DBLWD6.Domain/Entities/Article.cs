namespace DBLWD6.Domain.Entities
{
    public class Article : DbEntity
    {
        [NonNull]
        public string Title { get; set; }
        [NonNull]
        public string Text { get; set; }
        [NonNull]
        public string Photo { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}
