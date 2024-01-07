using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponAPI.Models.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
