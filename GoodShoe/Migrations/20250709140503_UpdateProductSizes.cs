using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodShoe.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductSizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update existing products with more realistic size availability
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                column: "AvailableSizes",
                value: "8,9,10,11,12,13");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                column: "AvailableSizes",
                value: "8,9,10,11,12");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                column: "AvailableSizes",
                value: "10,11,12,13,14,15");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4,
                column: "AvailableSizes",
                value: "8,9,10,11,12,13");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5,
                column: "AvailableSizes",
                value: "9,10,11,12,13,14");
            
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6,
                column: "AvailableSizes",
                value: "10,11,12,13,14,15,16");
            
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
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                column: "AvailableSizes",
                value: "8,9,10,11,12");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                column: "AvailableSizes",
                value: "8,9,10,11");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                column: "AvailableSizes",
                value: "10,11,12,13,14");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4,
                column: "AvailableSizes",
                value: "8,9,10,11,12");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5,
                column: "AvailableSizes",
                value: "9,10,11,12,13");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6,
                column: "AvailableSizes",
                value: "10,11,12,13,14,15");
            
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