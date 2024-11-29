namespace DBLWD6.API.Entities
{
    public class Partner : DbEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string? Name { get; set; }

        [NonNull]
        public string Image { get; set; } = "images/partners/default.png";

        [NonNull]
        public string Website { get; set; } = "https://minsk-lada.by";
    }
}
