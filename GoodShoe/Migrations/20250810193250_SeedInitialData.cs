using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodShoe.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert Admin
            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "UserName", "Phone", "DOB", "Email", "Currency", "UpdatedAt", "CreatedAt", "Password" },
                values: new object[] { "Admin User", "+65 1234 5678", new DateTime(1990, 1, 1), "admin@goodshoe.com", "SGD", new DateTime(2024, 1, 1), new DateTime(2024, 1, 1), "admin321" });

            // Insert Products (without Image data - will be handled by DbInitializer)
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Name", "Brand", "Price", "Description", "Color", "Category", "ImageUrl", "ImageFileName", "CreatedAt" },
                values: new object[,]
                {
                    { 1, "Cloudsurfer Next", "Puma", 259.00m, "Lace up in Swiss-engineered running shoes designed for the streets.", "White", "Unisex", "/images/products/image1.png", "image1.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 2, "Aero Burst", "Sketchers", 150.00m, "Hit every mile marker in long-distance comfort. Burst out with our Aero Burst Shoe and win the Run!", "Periwinkle", "Women", "/images/products/image2.png", "image2.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 3, "GoodShoe 0.1", "GoodShoe", 100.00m, "Men's Shoes", "Brown", "Men", "/images/products/image3.png", "image3.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 4, "GoodShoe 0.2", "GoodShoe", 120.00m, "Updated Women's Shoes", "Brown", "Women", "/images/products/image4.png", "image4.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 5, "GoodShoe 0.3", "GoodShoe", 110.00m, "New Unisex Shoes", "Blue", "Unisex", "/images/products/image5.png", "image5.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 6, "Nike Max 260", "Nike", 149.99m, "The Nike Air Max 270 delivers visible cushioning.", "White", "Men", "/images/products/nike-max-260.png", "nike-max-260.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 7, "Nike Free Metcon 6", "Nike", 120.00m, "Men's workout shoes designed for functional fitness training.", "Black/White", "Men", "/images/products/nike-free-metcon.png", "nike-free-metcon.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 8, "Nike Zoom Vomero S", "Nike", 160.00m, "Premium running shoes with Zoom Air cushioning.", "Gray/Blue", "Unisex", "/images/products/nike-zoom-vomero.png", "nike-zoom-vomero.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 9, "NY 90 Shoes", "Adidas", 85.00m, "Retro-inspired lifestyle sneakers.", "White/Navy", "Unisex", "/images/products/adidas-ny-90.png", "adidas-ny-90.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 10, "Old Skool", "Vans", 65.00m, "Skate shoe with signature side stripe.", "Navy/White", "Men", "/images/products/OldSkool.png", "OldSkool.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 11, "Air Jordan 1", "Nike", 170.00m, "Legendary basketball sneaker with premium materials.", "Red/Black/White", "Men", "/images/products/AirJordan1.png", "AirJordan1.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 12, "Ultraboost 22", "Adidas", 180.00m, "Premium running shoe with Boost midsole technology.", "Core Black", "Men", "/images/products/ultraboost.png", "ultraboost.png", new DateTime(2025, 8, 9, 23, 20, 27) },
                    { 13, "Classic Leather", "Reebok", 70.00m, "Timeless leather sneaker with clean lines.", "White", "Men", "/images/products/ClassicLeather.png", null, new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 14, "Air Force 1", "Nike", 90.00m, "Classic basketball-inspired sneaker.", "White", "Women", "/images/products/air-force1-women.png", "air-force1-women.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 15, "Superstar", "Adidas", 85.00m, "Iconic shell-toe sneaker with three stripes.", "White/Black", "Women", "/images/products/superstar.png", "superstar.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 16, "Platform Chuck Taylor", "Converse", 70.00m, "Elevated version of the classic Chuck Taylor.", "Black", "Women", "/images/products/platform-chuck-taylor.png", "platform-chuck-taylor.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 17, "Club C 85", "Reebok", 75.00m, "Vintage-inspired tennis shoe with clean design.", "White/Green", "Women", "/images/products/ClubC85.png", "ClubC85.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 18, "React Element 55", "Nike", 130.00m, "Modern lifestyle sneaker with React foam cushioning.", "Black/White", "Unisex", "/images/products/react-element-55.png", "react-element-55.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 19, "Blazer Mid", "Nike", 100.00m, "Vintage basketball shoe with modern comfort.", "White/Black", "Unisex", "/images/products/blazer-mid.png", "blazer-mid.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 20, "Continental 80", "Adidas", 80.00m, "Retro tennis shoe with vintage appeal.", "White/Red", "Unisex", "/images/products/continental-80.png", "continental-80.png", new DateTime(2025, 8, 1, 13, 12, 13) },
                    { 21, "Test Product", "Testing", 100.00m, "Test", "Test Color", "Men", null, "f8c6b24a-1685-4e04-a8b8-3fd2ee3a469c-removebg-preview.png", new DateTime(2025, 8, 10, 0, 39, 40) }
                });

            // Insert Customers
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "FirstName", "LastName", "Email", "Phone", "Address", "CreatedAt", "UpdatedAt", "Password" },
                values: new object[,]
                {
                    { "John", "Doe", "john.doe@email.com", "+65 9876 5432", "456 Customer Road, Singapore", new DateTime(2024, 1, 15), new DateTime(2024, 1, 15), "password123" },
                    { "Jane", "Smith", "jane.smith@email.com", "+65 8765 4321", "789 Shopper Lane, Singapore", new DateTime(2024, 1, 16), new DateTime(2024, 1, 16), "password123" },
                    { "Mike", "Johnson", "mike.johnson@email.com", "+65 9345 6789", "789 Sentosa Island, Singapore 098765", new DateTime(2024, 4, 1, 10, 0, 0), new DateTime(2025, 7, 30, 21, 25, 19, 490, 0), "password123" },
                    { "Sarah", "Wilson", "sarah.wilson@email.com", "+65 9456 7890", "321 Clarke Quay, Singapore 179024", new DateTime(2024, 5, 1, 8, 0, 0), new DateTime(2025, 7, 30, 21, 25, 19, 490, 0), "password123" },
                    { "David", "Lee", "david.lee@email.com", "+65 9567 8901", "654 Chinatown, Singapore 058357", new DateTime(2024, 6, 1, 11, 0, 0), new DateTime(2025, 7, 30, 21, 25, 19, 490, 0), "password123" },
                    { "Lisa", "Wong", "lisa.wong@email.com", "+65 9678 9012", "987 Little India, Singapore 209695", new DateTime(2024, 7, 1, 10, 0, 0), new DateTime(2025, 7, 30, 21, 25, 19, 490, 0), "password123" },
                    { "Tom", "Brown", "tom.brown@email.com", "+65 9789 0123", "147 Bugis Street, Singapore 188735", new DateTime(2024, 8, 1, 12, 0, 0), new DateTime(2025, 7, 30, 21, 25, 19, 490, 0), "password123" },
                    { "Emma", "Davis", "emma.davis@email.com", "+65 9890 1234", "258 Holland Village, Singapore 275832", new DateTime(2024, 9, 1, 6, 0, 0), new DateTime(2025, 7, 30, 21, 25, 19, 490, 0), "password123" },
                    { "Alex", "Chen", "alex.chen@email.com", "+65 9012 3456", "369 Tampines Mall, Singapore 529510", new DateTime(2024, 10, 1, 7, 0, 0), new DateTime(2025, 7, 30, 21, 25, 19, 490, 0), "password123" },
                    { "Grace", "Tan", "grace.tan@email.com", "+65 8123 4567", "741 Jurong Point, Singapore 648886", new DateTime(2024, 11, 1, 10, 0, 0), new DateTime(2025, 7, 30, 21, 25, 19, 490, 0), "password123" },
                    { "Lin", "Vulpes", "something@gmail.com", "+65 9123 4567", "Yishun Ring Road, Khatib", new DateTime(2025, 8, 10, 12, 29, 43), new DateTime(2025, 8, 10, 23, 49, 11), "something" }
                });

            // Insert ProductVariants - Simplified approach
            migrationBuilder.InsertData(
                table: "ProductVariant",
                columns: new[] { "ProductId", "Size", "StockCount" },
                values: new object[,]
                {
                    // Product 1 - Cloudsurfer Next (sizes 8-12)
                    { 1, 8, 7 }, { 1, 9, 3 }, { 1, 10, 3 }, { 1, 11, 3 }, { 1, 12, 3 },
                    
                    // Product 2 - Aero Burst (sizes 8-11)
                    { 2, 8, 5 }, { 2, 9, 7 }, { 2, 10, 7 }, { 2, 11, 17 },
                    
                    // Product 3 - GoodShoe 0.1 (sizes 10-14)
                    { 3, 10, 5 }, { 3, 11, 5 }, { 3, 12, 5 }, { 3, 13, 5 }, { 3, 14, 5 },
                    
                    // Product 4 - GoodShoe 0.2 (sizes 8-12)
                    { 4, 8, 8 }, { 4, 9, 8 }, { 4, 10, 8 }, { 4, 11, 8 }, { 4, 12, 8 },
                    
                    // Product 5 - GoodShoe 0.3 (sizes 8, 12, 16)
                    { 5, 8, 8 }, { 5, 12, 12 }, { 5, 16, 16 },
                    
                    // Product 6 - Nike Max 260 (sizes 10-15)
                    { 6, 10, 15 }, { 6, 11, 15 }, { 6, 12, 15 }, { 6, 13, 15 }, { 6, 14, 15 }, { 6, 15, 15 },
                    
                    // Product 7 - Nike Free Metcon 6
                    { 7, 8, 7 }, { 7, 9, 5 }, { 7, 10, 10 }, { 7, 12, 2 }, { 7, 15, 17 },
                    
                    // Product 8 - Nike Zoom Vomero S
                    { 8, 8, 10 }, { 8, 9, 15 }, { 8, 10, 15 }, { 8, 11, 15 }, { 8, 12, 10 }, { 8, 13, 5 }, { 8, 14, 5 },
                    
                    // Product 9 - NY 90 Shoes
                    { 9, 8, 10 }, { 9, 9, 15 }, { 9, 10, 15 }, { 9, 11, 15 }, { 9, 12, 10 }, { 9, 13, 5 }, { 9, 14, 5 },
                    
                    // Product 10 - Old Skool (out of stock)
                    { 10, 9, 0 },
                    
                    // Product 11 - Air Jordan 1 (limited stock)
                    { 11, 8, 0 }, { 11, 14, 0 },
                    
                    // Product 12 - Ultraboost 22
                    { 12, 8, 5 }, { 12, 10, 10 }, { 12, 11, 11 }, { 12, 12, 12 }, { 12, 13, 13 }, { 12, 14, 14 },
                    
                    // Product 13 - Classic Leather
                    { 13, 8, 10 }, { 13, 9, 15 }, { 13, 10, 15 }, { 13, 11, 15 }, { 13, 12, 10 }, { 13, 13, 5 }, { 13, 14, 5 },
                    
                    // Product 14 - Air Force 1
                    { 14, 8, 10 }, { 14, 9, 0 },
                    
                    // Product 15 - Superstar
                    { 15, 8, 10 }, { 15, 9, 15 }, { 15, 10, 15 }, { 15, 11, 15 }, { 15, 12, 10 }, { 15, 13, 5 }, { 15, 14, 5 },
                    
                    // Product 16 - Platform Chuck Taylor
                    { 16, 8, 10 }, { 16, 9, 15 }, { 16, 10, 0 }, { 16, 11, 15 }, { 16, 12, 11 }, { 16, 13, 5 }, { 16, 14, 5 },
                    
                    // Product 17 - Club C 85
                    { 17, 8, 10 }, { 17, 9, 15 }, { 17, 10, 15 }, { 17, 11, 15 }, { 17, 12, 10 }, { 17, 13, 5 }, { 17, 14, 5 },
                    
                    // Product 18 - React Element 55
                    { 18, 8, 10 }, { 18, 9, 15 }, { 18, 10, 15 }, { 18, 11, 15 }, { 18, 12, 10 }, { 18, 13, 5 }, { 18, 14, 5 },
                    
                    // Product 19 - Blazer Mid
                    { 19, 8, 10 }, { 19, 9, 15 }, { 19, 10, 5 }, { 19, 11, 15 }, { 19, 12, 10 }, { 19, 13, 5 }, { 19, 14, 5 },
                    
                    // Product 20 - Continental 80
                    { 20, 8, 1 }, { 20, 9, 10 }, { 20, 10, 3 }, { 20, 12, 3 }, { 20, 15, 0 },
                    
                    // Product 21 - Test Product
                    { 21, 8, 3 }, { 21, 9, 5 }, { 21, 10, 10 }, { 21, 12, 12 }
                });

            // Insert sample Orders (first few to establish structure)
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "CustomerId", "TotalAmount", "Status", "Address", "PaymentMethod", "CreatedAt", "PaymentStatus", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 409.00m, "Pending", "456 Customer Road, Singapore", "Credit Card", new DateTime(2024, 7, 4), "Pending", new DateTime(2024, 7, 4) },
                    { 2, 150.00m, "Shipped", "789 Shopper Lane, Singapore", "PayPal", new DateTime(2024, 7, 5), "Completed", new DateTime(2024, 7, 5) },
                    { 1, 259.00m, "Delivered", "456 Customer Road, Singapore", "Credit Card", new DateTime(2024, 7, 1), "Completed", new DateTime(2024, 7, 1) }
                });

            // Insert sample OrderItems
            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderId", "ProductVariantId", "ProductName", "Quantity", "UnitPrice", "Size", "TotalPrice" },
                values: new object[,]
                {
                    { 1, 1, "Cloudsurfer Next", 1, 259.00m, 8, 259.00m },   // Product 1, Size 8
                    { 1, 5, "Aero Burst", 1, 150.00m, 8, 150.00m },        // Product 2, Size 8  
                    { 2, 6, "Aero Burst", 1, 150.00m, 9, 150.00m },        // Product 2, Size 9
                    { 3, 2, "Cloudsurfer Next", 1, 259.00m, 9, 259.00m }   // Product 1, Size 9
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete data in reverse order using Entity Framework only

            // Delete OrderItems - using specific IDs that will be auto-generated
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4 });

            // Delete Orders
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId", 
                keyValues: new object[] { 1, 2, 3 });
            
            // Delete ProductVariants - Need to delete by ProductId and Size combinations
            migrationBuilder.DeleteData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[,]
                {
                    // Product 1 variants
                    { 1, 8 }, { 1, 9 }, { 1, 10 }, { 1, 11 }, { 1, 12 },
                    // Product 2 variants
                    { 2, 8 }, { 2, 9 }, { 2, 10 }, { 2, 11 },
                    // Product 3 variants
                    { 3, 10 }, { 3, 11 }, { 3, 12 }, { 3, 13 }, { 3, 14 },
                    // Product 4 variants
                    { 4, 8 }, { 4, 9 }, { 4, 10 }, { 4, 11 }, { 4, 12 },
                    // Product 5 variants
                    { 5, 8 }, { 5, 12 }, { 5, 16 },
                    // Product 6 variants
                    { 6, 10 }, { 6, 11 }, { 6, 12 }, { 6, 13 }, { 6, 14 }, { 6, 15 },
                    // Product 7 variants
                    { 7, 8 }, { 7, 9 }, { 7, 10 }, { 7, 12 }, { 7, 15 },
                    // Product 8 variants
                    { 8, 8 }, { 8, 9 }, { 8, 10 }, { 8, 11 }, { 8, 12 }, { 8, 13 }, { 8, 14 },
                    // Product 9 variants
                    { 9, 8 }, { 9, 9 }, { 9, 10 }, { 9, 11 }, { 9, 12 }, { 9, 13 }, { 9, 14 },
                    // Product 10 variants
                    { 10, 9 },
                    // Product 11 variants
                    { 11, 8 }, { 11, 14 },
                    // Product 12 variants
                    { 12, 8 }, { 12, 10 }, { 12, 11 }, { 12, 12 }, { 12, 13 }, { 12, 14 },
                    // Product 13 variants
                    { 13, 8 }, { 13, 9 }, { 13, 10 }, { 13, 11 }, { 13, 12 }, { 13, 13 }, { 13, 14 },
                    // Product 14 variants
                    { 14, 8 }, { 14, 9 },
                    // Product 15 variants
                    { 15, 8 }, { 15, 9 }, { 15, 10 }, { 15, 11 }, { 15, 12 }, { 15, 13 }, { 15, 14 },
                    // Product 16 variants
                    { 16, 8 }, { 16, 9 }, { 16, 10 }, { 16, 11 }, { 16, 12 }, { 16, 13 }, { 16, 14 },
                    // Product 17 variants
                    { 17, 8 }, { 17, 9 }, { 17, 10 }, { 17, 11 }, { 17, 12 }, { 17, 13 }, { 17, 14 },
                    // Product 18 variants
                    { 18, 8 }, { 18, 9 }, { 18, 10 }, { 18, 11 }, { 18, 12 }, { 18, 13 }, { 18, 14 },
                    // Product 19 variants
                    { 19, 8 }, { 19, 9 }, { 19, 10 }, { 19, 11 }, { 19, 12 }, { 19, 13 }, { 19, 14 },
                    // Product 20 variants
                    { 20, 8 }, { 20, 9 }, { 20, 10 }, { 20, 12 }, { 20, 15 },
                    // Product 21 variants
                    { 21, 8 }, { 21, 9 }, { 21, 10 }, { 21, 12 }
                });
            
            // Delete Products
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId", 
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 });
            
            // Delete Customers
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 });
            
            // Delete Admin
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "AdminId",
                keyValue: 1);
        }
    }
}