namespace GeekShopping.Web.Models
{
    public class CartDetailViewModel
    {
        public Int64 Id { get; set; }
        public Int64 CartHeaderId { get; set; }
        public CartHeaderViewModel CartHeader { get; set; }
        public Int64 ProductId { get; set; }
        public ProductViewModel Product { get; set; }
        public int Count { get; set; }
    }
}
