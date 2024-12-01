namespace DBLWD6.Domain.Entities
{
    public class User : DbEntity
    {
        [NonNull]
        public string Password { get; set; }

        public DateTime? LastLogin { get; set; }

        [NonNull]
        public bool IsSuperuser { get; set; }

        [NonNull]
        [Unique]
        public string Username { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [NonNull]
        [Unique]
        public string Email { get; set; }

        [NonNull]
        public bool IsStaff { get; set; }

        [NonNull]
        public bool IsActive { get; set; }

        [NonNull]
        public DateTime DateJoined { get; set; }

        [NonMapped]
        public List<Review> Reviews { get; set; }
    }
}
