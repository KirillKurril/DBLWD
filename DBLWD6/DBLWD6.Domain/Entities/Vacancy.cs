namespace DBLWD6.Domain.Entities
{
    public class Vacancy : DbEntity
    {
        [NonNull]
        public string Title { get; set; }
        public string Description { get; set; }
        [NonNull]
        public string Requirements { get; set; }
        public string Responsibilities { get; set; }
        public string Location { get; set; }
        [NonNull]
        public double Salary { get; set; }
        public string Image {  get; set; }
        [NonNull]
        public DateTime CreatedAt { get; set; }
    }
}
