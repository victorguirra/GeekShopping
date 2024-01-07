using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponAPI.Models.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon()
            {
                Id = 1,
                CouponCode = "GUIRRA_2024_10",
                DiscountAmout = 10
            });
            
            modelBuilder.Entity<Coupon>().HasData(new Coupon()
            {
                Id = 2,
                CouponCode = "GUIRRA_2024_15",
                DiscountAmout = 15
            });
        }
    }
}
