namespace DBLWD6.Domain.Entities
{
    public class PromoCode : DbEntity
    {
        [NonNull]
        public int Code { get; set; }
        [NonNull]
        public double DisCount { get; set; }
        [NonNull]
        public DateTime Expiration {  get; set; }
    }
}
