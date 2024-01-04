﻿using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Models.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(){ }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options){ }

    
        public DbSet<Product> Products { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
    }
}
