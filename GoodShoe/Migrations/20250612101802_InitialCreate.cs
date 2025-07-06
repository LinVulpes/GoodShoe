using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodShoe.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    StockCount = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    // Note: Size column will be removed in SimpleECommerceStructure migration
                    Size = table.Column<decimal>(type: "decimal(3,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Gender",
                table: "Product",
                column: "Gender");

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Name", "Brand", "Price", "Size", "Description", "StockCount", "Color", "Gender", "ImageUrl" },
                values: new object[,]
                {
                    { 1, "Cloudsurfer Next", "Puma", 259.00m, 7.0m, "Lace up in Swiss-engineered runners with these Cloudsurfer Next trainers from On Running.", 3, "White", "Unisex", "/images/products/image1.png" },
                    { 2, "Aero Burst", "Sketchers", 150.00m, 6.5m, "Hit every mile marker in long-distance confidence and premium cushioned comfort with Skechers Aero Burst™.", 7, "Periwinkle", "Women", "/images/products/image2.png" },
                    { 3, "GoodShoe 0.1", "GoodShoe", 100.00m, 8.0m, "Men's Shoes", 5, "Brown", "Men", "/images/products/image3.png" },
                    { 4, "GoodShoe 0.2", "GoodShoe", 120.00m, 8.0m, "Updated Women's Shoes", 8, "Brown", "Women", "/images/products/image4.png" },
                    { 5, "GoodShoe 0.3", "GoodShoe", 110.00m, 9.0m, "New Unisex Shoes", 6, "Blue", "Unisex", "/images/products/image5.png" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5 });

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}