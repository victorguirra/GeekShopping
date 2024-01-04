using GeekShopping.CartAPI.Models.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GeekShopping.CartAPI.Models
{
    [Table("PRODUCT")]
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID")]
        public Int64 Id { get; set; }

        [Column("NAME")]
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Column("PRICE", TypeName = "decimal(18,2)")]
        [Required]
        [Range(1, 10000)]
        public decimal Price { get; set; }

        [Column("DESCRIPTION")]
        [StringLength(500)]
        public string Description { get; set; }

        [Column("CATEGORY_NAME")]
        [StringLength(50)]
        public string CategoryName { get; set; }

        [Column("IMAGE_URL")]
        [StringLength(300)]
        public string ImageURL { get; set; }
    }
}
