namespace DBLWD6.API.Entities
{
    public class Profile : DbEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        [NonNull]
        [Unique]
        [ForeignKey(typeof(User), "Id")]
        public int UserId { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? PhoneNumber { get; set; }

        public string Photo { get; set; } = "images/employees/default_employee.png";

        public string? JobDescription { get; set; }

        public bool NonSecretive { get; set; } = false;
    }
}
