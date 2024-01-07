namespace GeekShopping.CouponAPI.Data.ValueObjects
{
    public class CouponVO
    {
        public Int64 Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
