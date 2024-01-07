using GeekShopping.CouponAPI.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CouponAPI.Models
{
    [Table("COUPON")]
    public class Coupon : BaseEntity
    {
        [Column("COUPON_CODE")]
        [Required]
        [StringLength(30)]
        public string CouponCode { get; set; }

        [Column("DISCOUNT_AMOUNT")]
        [Required]
        public decimal DiscountAmout { get; set; }
    }
}
