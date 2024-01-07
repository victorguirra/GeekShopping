using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeekShopping.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedCouponDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "COUPON",
                columns: new[] { "ID", "COUPON_CODE", "DISCOUNT_AMOUNT" },
                values: new object[,]
                {
                    { 1L, "GUIRRA_2024_10", 10m },
                    { 2L, "GUIRRA_2024_15", 15m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "COUPON",
                keyColumn: "ID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "COUPON",
                keyColumn: "ID",
                keyValue: 2L);
        }
    }
}
