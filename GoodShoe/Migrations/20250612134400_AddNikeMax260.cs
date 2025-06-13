using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodShoe.Migrations
{
    /// <inheritdoc />
    public partial class AddNikeMax260 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Using EF's InsertData method (more type-safe)
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Name", "Brand", "Price", "Size", "Description", "StockCount", "Color", "Gender", "ImageUrl" },
                values: new object[] { 
                    "Nike Max 260", 
                    "Nike", 
                    149.99m, 
                    9.0m, 
                    "The Nike Air Max 270 delivers visible cushioning under every step.", 
                    15, 
                    "White", 
                    "Men", 
                    "/images/products/image6.png" 
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Removing the specific product
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name",
                keyValue: "Nike Max 260");
        }
    }
}
