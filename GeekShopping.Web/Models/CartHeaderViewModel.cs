namespace GeekShopping.Web.Models
{
    public class CartHeaderViewModel
    {
        public Int64 Id { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public decimal PurchaseAmount { get; set; }
    }
}
