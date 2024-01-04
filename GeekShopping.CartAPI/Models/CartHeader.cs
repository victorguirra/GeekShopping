using GeekShopping.CartAPI.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartAPI.Models
{
    [Table("CARTHEADER")]
    public class CartHeader : BaseEntity
    {
        [Column("USER_ID")]
        public string UserId { get; set; }
        
        [Column("COUPON_CODE")]
        public string CouponCode { get; set; }
    }
}
