namespace DBLWD6.Domain.Entities
{
    public class PromoCode : DbEntity
    {
        [NonNull]
        int Code { get; set; }
        [NonNull]
        double DisCount { get; set; }
        [NonNull]
        DateTime Expiration {  get; set; }
    }
}
