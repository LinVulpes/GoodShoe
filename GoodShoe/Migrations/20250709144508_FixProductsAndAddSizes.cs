using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodShoe.Migrations
{
    /// <inheritdoc />
    public partial class FixProductsAndAddSizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove duplicate Nike Max 260 (ID 1006)
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1006);

            // Update all new products with AvailableSizes data using UpdateData
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Nike Free Metcon 6", "Nike" },
                column: "AvailableSizes",
                value: "10,11,12,13,14,15");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Nike Zoom Vomero 5", "Nike" },
                column: "AvailableSizes",
                value: "8,9,10,11,12,13,14");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "NY 90 Shoes", "Adidas" },
                column: "AvailableSizes",
                value: "8,9,10,11,12,13");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Old Skool", "Vans" },
                column: "AvailableSizes",
                value: "9,10,11,12,13,14");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Air Jordan 1", "Nike" },
                column: "AvailableSizes",
                value: "9,10,11,12,13,14,15");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Ultraboost 22", "Adidas" },
                column: "AvailableSizes",
                value: "10,11,12,13,14");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Classic Leather", "Reebok" },
                column: "AvailableSizes",
                value: "9,10,11,12,13,14,15");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Air Force 1", "Nike" },
                column: "AvailableSizes",
                value: "8,9,10,11,12");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Superstar", "Adidas" },
                column: "AvailableSizes",
                value: "8,9,10,11,12,13");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Platform Chuck Taylor", "Converse" },
                column: "AvailableSizes",
                value: "8,9,10,11");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Club C 85", "Reebok" },
                column: "AvailableSizes",
                value: "8,9,10,11,12");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "React Element 55", "Nike" },
                column: "AvailableSizes",
                value: "8,9,10,11,12,13,14");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Blazer Mid", "Nike" },
                column: "AvailableSizes",
                value: "9,10,11,12,13");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Continental 80", "Adidas" },
                column: "AvailableSizes",
                value: "8,9,10,11,12,13,14");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Name", "Brand", "Price", "Description", "StockCount", "Color", "Category", "ImageUrl", "AvailableSizes" },
                values: new object[] { 1006, "Nike Max 260", "Nike", 149.99m, "The Nike Air Max 270 delivers visible cushioning under eve", 15, "Black", "Men", "/images/products/nike-max-260.png", "9,10,11,12,13" });
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Nike Free Metcon 6", "Nike" },
                column: "AvailableSizes",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Nike Zoom Vomero 5", "Nike" },
                column: "AvailableSizes",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "NY 90 Shoes", "Adidas" },
                column: "AvailableSizes",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Old Skool", "Vans" },
                column: "AvailableSizes",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Air Jordan 1", "Nike" },
                column: "AvailableSizes",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Ultraboost 22", "Adidas" },
                column: "AvailableSizes",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Classic Leather", "Reebok" },
                column: "AvailableSizes",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Air Force 1", "Nike" },
                column: "AvailableSizes",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Superstar", "Adidas" },
                column: "AvailableSizes",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Platform Chuck Taylor", "Converse" },
                column: "AvailableSizes",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Club C 85", "Reebok" },
                column: "AvailableSizes",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "React Element 55", "Nike" },
                column: "AvailableSizes",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Blazer Mid", "Nike" },
                column: "AvailableSizes",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumns: new[] { "Name", "Brand" },
                keyValues: new object[] { "Continental 80", "Adidas" },
                column: "AvailableSizes",
                value: "");
        }
    }
}