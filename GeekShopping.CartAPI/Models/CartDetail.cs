using GeekShopping.CartAPI.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartAPI.Models
{
    [Table("CARTDETAIL")]
    public class CartDetail : BaseEntity
    {
        [Column("CART_HEADER_ID")]
        public Int64 CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }

        [Column("PRODUCT_ID")]
        public Int64 ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Column("COUNT")]
        public int Count { get; set; }
    }
}
