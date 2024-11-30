namespace DBLWD6.Domain.Entities
{
    public class Order
    {
        [NonNull]
        public DateTime Date {  get; set; }

        [NonNull]
        public int Quantity { get; set; }

        [NonNull]
        [ForeignKey(typeof(Product), "Id")]
        public int ProductId { get; set; }

        [NonNull]
        [ForeignKey(typeof(User), "Id")]
        public int UserId { get; set; }

        [NonNull]
        [ForeignKey(typeof(PickupPoint), "Id")]
        public int PickupPointId { get; set; }

        [ForeignKey(typeof(PromoCode), "Id")]
        public int PromoCodeId { get; set; }
    }
}
