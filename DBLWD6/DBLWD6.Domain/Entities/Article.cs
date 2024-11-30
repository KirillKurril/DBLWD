namespace DBLWD6.Domain.Entities
{
    public class Article : DbEntity
    {
        [NonNull]
        string Title { get; set; }
        [NonNull]
        string Text { get; set; }
        [NonNull]
        string Photo { get; set; }
        DateTime CreatedAt { get; set; }
        
    }
}
