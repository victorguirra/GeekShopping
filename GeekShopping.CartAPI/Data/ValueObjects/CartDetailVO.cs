namespace GeekShopping.CartAPI.Data.ValueObjects
{
    public class CartDetailVO
    {
        public Int64 Id { get; set; }
        public Int64 CartHeaderId { get; set; }
        public CartHeaderVO? CartHeader { get; set; }
        public Int64 ProductId { get; set; }
        public ProductVO Product { get; set; }
        public int Count { get; set; }
    }
}
