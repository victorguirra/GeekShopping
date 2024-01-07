using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GeekShopping.CouponAPI.Models.Base
{
    public class BaseEntity
    {
        [Key]
        [Column("ID")]
        public Int64 Id { get; set; }
    }
}
