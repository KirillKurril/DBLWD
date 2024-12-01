namespace DBLWD6.Domain.Entities
{
    public class Review : DbEntity
    {
        [NonNull]
        public string Title { get; set; } = "Not definded";

        [NonNull]
        public string Text { get; set; } = "Not definded";

        [NonNull]
        public int Rating { get; set; } = 5;

        [NonNull]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [NonNull]
        [ForeignKey(typeof(User), "Id")]
        public int UserId { get; set; }
    }
}
