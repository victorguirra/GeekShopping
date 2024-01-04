using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartAPI.Models.Base
{
    public class BaseEntity
    {
        [Key]
        [Column("ID")]
        public Int64 Id { get; set; }
    }
}
