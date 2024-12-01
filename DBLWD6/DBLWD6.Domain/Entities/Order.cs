namespace DBLWD6.Domain.Entities
{
    public class Order : DbEntity
    {
        [NonNull]
        public DateTime Date {  get; set; }

        [NonNull]
        public int Quantity { get; set; }

        [NonNull]
        [ForeignKey(typeof(Product), "Id")]
        public int ProductId { get; set; }

        [NonMapped]
        public Product Product { get; set; }

        [NonNull]
        [ForeignKey(typeof(User), "Id")]
        public int UserId { get; set; }

        [NonMapped]
        public User User { get; set; }

        [NonNull]
        [ForeignKey(typeof(PickupPoint), "Id")]
        public int PickupPointId { get; set; }

        [NonMapped]
        public PickupPoint PickupPoint { get; set; }

        [ForeignKey(typeof(PromoCode), "Id")]
        public int? PromoCodeId { get; set; }

        [NonMapped]
        public PromoCode PromoCode { get; set; }
    }
}
