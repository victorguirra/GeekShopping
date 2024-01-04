using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShopping.CartAPI.Migrations
{
    /// <inheritdoc />
    public partial class changeColumnsName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CARTDETAIL_CARTHEADER_CartHeaderId",
                table: "CARTDETAIL");

            migrationBuilder.DropForeignKey(
                name: "FK_CARTDETAIL_PRODUCT_ProductId",
                table: "CARTDETAIL");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "CARTDETAIL",
                newName: "PRODUCT_ID");

            migrationBuilder.RenameColumn(
                name: "CartHeaderId",
                table: "CARTDETAIL",
                newName: "CART_HEADER_ID");

            migrationBuilder.RenameIndex(
                name: "IX_CARTDETAIL_ProductId",
                table: "CARTDETAIL",
                newName: "IX_CARTDETAIL_PRODUCT_ID");

            migrationBuilder.RenameIndex(
                name: "IX_CARTDETAIL_CartHeaderId",
                table: "CARTDETAIL",
                newName: "IX_CARTDETAIL_CART_HEADER_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CARTDETAIL_CARTHEADER_CART_HEADER_ID",
                table: "CARTDETAIL",
                column: "CART_HEADER_ID",
                principalTable: "CARTHEADER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CARTDETAIL_PRODUCT_PRODUCT_ID",
                table: "CARTDETAIL",
                column: "PRODUCT_ID",
                principalTable: "PRODUCT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CARTDETAIL_CARTHEADER_CART_HEADER_ID",
                table: "CARTDETAIL");

            migrationBuilder.DropForeignKey(
                name: "FK_CARTDETAIL_PRODUCT_PRODUCT_ID",
                table: "CARTDETAIL");

            migrationBuilder.RenameColumn(
                name: "PRODUCT_ID",
                table: "CARTDETAIL",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "CART_HEADER_ID",
                table: "CARTDETAIL",
                newName: "CartHeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_CARTDETAIL_PRODUCT_ID",
                table: "CARTDETAIL",
                newName: "IX_CARTDETAIL_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CARTDETAIL_CART_HEADER_ID",
                table: "CARTDETAIL",
                newName: "IX_CARTDETAIL_CartHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CARTDETAIL_CARTHEADER_CartHeaderId",
                table: "CARTDETAIL",
                column: "CartHeaderId",
                principalTable: "CARTHEADER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CARTDETAIL_PRODUCT_ProductId",
                table: "CARTDETAIL",
                column: "ProductId",
                principalTable: "PRODUCT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
