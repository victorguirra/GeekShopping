﻿// <auto-generated />
using GeekShopping.CouponAPI.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeekShopping.CouponAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240107015334_SeedCouponDatabase")]
    partial class SeedCouponDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GeekShopping.CouponAPI.Models.Coupon", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CouponCode")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("COUPON_CODE");

                    b.Property<decimal>("DiscountAmout")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("DISCOUNT_AMOUNT");

                    b.HasKey("Id");

                    b.ToTable("COUPON");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CouponCode = "GUIRRA_2024_10",
                            DiscountAmout = 10m
                        },
                        new
                        {
                            Id = 2L,
                            CouponCode = "GUIRRA_2024_15",
                            DiscountAmout = 15m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
