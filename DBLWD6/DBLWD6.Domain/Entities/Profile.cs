namespace DBLWD6.Domain.Entities
{
    public class Profile : DbEntity
    {
        [NonNull]
        [Unique]
        [ForeignKey(typeof(User), "Id", "CASCADE")]
        public int UserId { get; set; }

        [NonMapped]
        public User User { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? PhoneNumber { get; set; }

        public string Photo { get; set; } = "images/employees/default_employee.png";

        public string? JobDescription { get; set; }

        public bool NonSecretive { get; set; } = false;
    }
}
