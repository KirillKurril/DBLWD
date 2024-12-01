namespace DBLWD6.Domain.Entities
{
    public class Partner : DbEntity
    {
        public string? Name { get; set; }

        [NonNull]
        public string Image { get; set; } = "images/partners/default.png";

        [NonNull]
        public string Website { get; set; } = "https://minsk-lada.by";
    }
}
