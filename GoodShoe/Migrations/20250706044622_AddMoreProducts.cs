using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodShoe.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert new products using InsertData (EF method)
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Name", "Brand", "Price", "Description", "StockCount", "Color", "Category", "ImageUrl" },
                values: new object[,]
                {
                    // Men's Shoes
                    { "Nike Free Metcon 6", "Nike", 120.00m, "Men's workout shoes designed for cross-training with flexible sole and durable construction for gym sessions.", 18, "Black/White", "Men", "/images/products/nike-free-metcon-6.png" },
                    { "Nike Zoom Vomero 5", "Nike", 160.00m, "Premium running shoes with Zoom Air cushioning technology for responsive comfort during long runs.", 12, "Gray/Blue", "Unisex", "/images/products/nike-zoom-vomero-5.png" },
                    { "NY 90 Shoes", "Adidas", 85.00m, "Retro-inspired lifestyle sneakers with classic New York aesthetic and modern comfort features.", 15, "White/Navy", "Unisex", "/images/products/adidas-ny-90.png"},
                    { "Old Skool", "Vans", 65.00m, "Skate shoe with signature side stripe and durable construction.", 18, "Navy/White", "Men", "/images/products/OldSkool.png" },
                    { "Air Jordan 1", "Nike", 170.00m, "Legendary basketball sneaker with iconic design and premium materials.", 8, "Red/Black/White", "Men", "/images/products/AirJordan1.png" },
                    { "Ultraboost 22", "Adidas", 180.00m, "Premium running shoe with boost technology for energy return.", 10, "Core Black", "Men", "/images/products/UltraBoost.png" },
                    { "Classic Leather", "Reebok", 70.00m, "Timeless leather sneaker with clean, simple design.", 14, "White", "Men", "/images/products/ClassLeather.png" },
                    
                    // Women's Shoes
                    { "Air Force 1", "Nike", 90.00m, "Classic basketball-inspired sneaker with versatile style.", 22, "White", "Women", "/images/products/air-force1-women.png"},
                    { "Superstar", "Adidas", 85.00m, "Iconic shell-toe sneaker with three stripes design.", 16, "White/Black", "Women", "/images/products/superstar.png" },
                    { "Platform Chuck Taylor", "Converse", 70.00m, "Elevated version of the classic Chuck Taylor with platform sole.", 11, "Black", "Women", "/images/products/platform-chuck-taylor.png"},
                    { "Club C 85", "Reebok", 75.00m, "Vintage-inspired tennis shoe with clean court styling.", 17, "White/Green", "Women", "/images/products/ClubC85.png" },
                    
                    // Unisex Shoes
                    { "React Element 55", "Nike", 130.00m, "Modern lifestyle sneaker with React foam for comfort.", 12, "Black/White", "Unisex", "/images/products/react-element-55.png" },
                    { "Blazer Mid", "Nike", 100.00m, "Vintage basketball shoe with swoosh logo and retro appeal.", 11, "White/Black", "Unisex", "/images/products/blazer-mid.png" },
                    { "Continental 80", "Adidas", 80.00m, "Retro tennis shoe with vintage appeal and modern comfort.", 14, "White/Red", "Unisex", "/images/products/continental-80.png" }
                });

            // Fix existing product categories using UpdateData
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4,
                column: "Category",
                value: "Women");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id", 
                keyValue: 5,
                column: "Category",
                value: "Unisex");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the added products by name - FIXED to match the actual product names being inserted
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name",
                keyValue: "Nike Free Metcon 6");
            
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name", 
                keyValue: "Nike Zoom Vomero 5");
                
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name",
                keyValue: "NY 90 Shoes");
                
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name",
                keyValue: "Old Skool");
                
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name",
                keyValue: "Air Jordan 1");
                
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name",
                keyValue: "Ultraboost 22");
                
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name",
                keyValue: "Classic Leather");
                
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name",
                keyValue: "Air Force 1");
                
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name",
                keyValue: "Superstar");
                
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name",
                keyValue: "Platform Chuck Taylor");
                
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name",
                keyValue: "Club C 85");
                
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name",
                keyValue: "React Element 55");
                
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name",
                keyValue: "Blazer Mid");
                
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name",
                keyValue: "Continental 80");
            
            // Revert category changes
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4,
                column: "Category",
                value: "Men");
                
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5,
                column: "Category",
                value: "Men");
        }
    }
}