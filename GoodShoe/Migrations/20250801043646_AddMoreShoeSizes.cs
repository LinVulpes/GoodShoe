using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodShoe.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreShoeSizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop foreign key constraint temporarily
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_Product_ProductId",
                table: "ProductVariant");

            // Step 2: Drop and recreate Product table with new structure (no identity)
            migrationBuilder.DropTable(name: "Product");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageFileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            // Step 3: Insert the product data with corrected IDs
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Name", "Brand", "Price", "Description", "Color", "Category", "ImageUrl" },
                values: new object[,]
                {
                    { 1, "Cloudsurfer Next", "Puma", 259.00m, "Lace up in Swiss-engineered running shoes designed for the streets.", "White", "Unisex", "/images/products/image1.png" },
                    { 2, "Aero Burst", "Sketchers", 150.00m, "Hit every mile marker in long-distance comfort.", "Periwinkle", "Women", "/images/products/image2.png" },
                    { 3, "GoodShoe 0.1", "GoodShoe", 100.00m, "Men's Shoes", "Brown", "Men", "/images/products/image3.png" },
                    { 4, "GoodShoe 0.2", "GoodShoe", 120.00m, "Updated Women's Shoes", "Brown", "Women", "/images/products/image4.png" },
                    { 5, "GoodShoe 0.3", "GoodShoe", 110.00m, "New Unisex Shoes", "Blue", "Unisex", "/images/products/image5.png" },
                    { 6, "Nike Max 260", "Nike", 149.99m, "The Nike Air Max 270 delivers visible cushioning.", "White", "Men", "/images/products/nike-max-260.png" },
                    { 7, "Nike Free Metcon 6", "Nike", 120.00m, "Men's workout shoes designed for functional fitness training.", "Black/White", "Men", "/images/products/nike-free-metcon.png" },
                    { 8, "Nike Zoom Vomero S", "Nike", 160.00m, "Premium running shoes with Zoom Air cushioning.", "Gray/Blue", "Unisex", "/images/products/nike-zoom-vomero.png" },
                    { 9, "NY 90 Shoes", "Adidas", 85.00m, "Retro-inspired lifestyle sneakers.", "White/Navy", "Unisex", "/images/products/adidas-ny-90.png" },
                    { 10, "Old Skool", "Vans", 65.00m, "Skate shoe with signature side stripe.", "Navy/White", "Men", "/images/products/OldSkool.png" },
                    { 11, "Air Jordan 1", "Nike", 170.00m, "Legendary basketball sneaker with premium materials.", "Red/Black/White", "Men", "/images/products/AirJordan1.png" },
                    { 12, "Ultraboost 22", "Adidas", 180.00m, "Premium running shoe with Boost midsole technology.", "Core Black", "Men", "/images/products/ultraboost.png" },
                    { 13, "Classic Leather", "Reebok", 70.00m, "Timeless leather sneaker with clean lines.", "White", "Men", "/images/products/ClassicLeather.png" },
                    { 14, "Air Force 1", "Nike", 90.00m, "Classic basketball-inspired sneaker.", "White", "Women", "/images/products/air-force1-women.png" },
                    { 15, "Superstar", "Adidas", 85.00m, "Iconic shell-toe sneaker with three stripes.", "White/Black", "Women", "/images/products/superstar.png" },
                    { 16, "Platform Chuck Taylor", "Converse", 70.00m, "Elevated version of the classic Chuck Taylor.", "Black", "Women", "/images/products/platform-chuck-taylor.png" },
                    { 17, "Club C 85", "Reebok", 75.00m, "Vintage-inspired tennis shoe with clean design.", "White/Green", "Women", "/images/products/ClubC85.png" },
                    { 18, "React Element 55", "Nike", 130.00m, "Modern lifestyle sneaker with React foam cushioning.", "Black/White", "Unisex", "/images/products/react-element-55.png" },
                    { 19, "Blazer Mid", "Nike", 100.00m, "Vintage basketball shoe with modern comfort.", "White/Black", "Unisex", "/images/products/blazer-mid.png" },
                    { 20, "Continental 80", "Adidas", 80.00m, "Retro tennis shoe with vintage appeal.", "White/Red", "Unisex", "/images/products/continental-80.png" }
                });

            // Step 4: Update existing ProductVariants to use new ProductIDs (if any exist)
            // This will be handled by your existing data

            // Step 5: Recreate foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_Product_ProductId",
                table: "ProductVariant",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_Product_ProductId",
                table: "ProductVariant");

            // Drop Product table
            migrationBuilder.DropTable(name: "Product");

            // Recreate Product table with identity column
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageFileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            // Restore original products with old IDs (simplified - just a few key ones)
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Name", "Brand", "Price", "Description", "Color", "Category", "ImageUrl" },
                values: new object[,]
                {
                    { 1007, "Nike Free Metcon 6", "Nike", 120.00m, "Men's workout shoes designed for functional fitness training.", "Black/White", "Men", "/images/products/nike-free-metcon.png" },
                    { 1008, "Nike Zoom Vomero S", "Nike", 160.00m, "Premium running shoes with Zoom Air cushioning.", "Gray/Blue", "Unisex", "/images/products/nike-zoom-vomero.png" },
                    { 1009, "NY 90 Shoes", "Adidas", 85.00m, "Retro-inspired lifestyle sneakers.", "White/Navy", "Unisex", "/images/products/adidas-ny-90.png" },
                    { 1010, "Old Skool", "Vans", 65.00m, "Skate shoe with signature side stripe.", "Navy/White", "Men", "/images/products/OldSkool.png" }
                });

            // Recreate foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_Product_ProductId",
                table: "ProductVariant",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}