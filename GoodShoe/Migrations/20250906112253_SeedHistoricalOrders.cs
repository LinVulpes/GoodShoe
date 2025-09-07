using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodShoe.Migrations
{
    /// <inheritdoc />
    public partial class SeedHistoricalOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert historical orders from 2024 to August 2025
            // Using existing customers (IDs 1-10) and real products (IDs 1-20, excluding test product 21)
            
            // January 2024 Orders
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "CustomerId", "TotalAmount", "Status", "Address", "PaymentMethod", "PaymentStatus", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { 3, 259.00m, "Delivered", "789 Sentosa Island, Singapore 098765", "Credit Card", "Completed", new DateTime(2024, 1, 5, 14, 30, 0), new DateTime(2024, 1, 8, 16, 45, 0) },
                    { 4, 150.00m, "Delivered", "321 Clarke Quay, Singapore 179024", "PayPal", "Completed", new DateTime(2024, 1, 12, 10, 15, 0), new DateTime(2024, 1, 15, 9, 30, 0) },
                    { 5, 170.00m, "Delivered", "654 Chinatown, Singapore 058357", "Apple Pay", "Completed", new DateTime(2024, 1, 18, 16, 20, 0), new DateTime(2024, 1, 22, 11, 10, 0) },
                    { 2, 320.00m, "Delivered", "789 Shopper Lane, Singapore", "Credit Card", "Completed", new DateTime(2024, 1, 25, 13, 45, 0), new DateTime(2024, 1, 28, 14, 20, 0) },
                    
                    // February 2024 Orders
                    { 6, 90.00m, "Delivered", "987 Little India, Singapore 209695", "Credit Card", "Completed", new DateTime(2024, 2, 3, 11, 30, 0), new DateTime(2024, 2, 7, 15, 45, 0) },
                    { 7, 180.00m, "Delivered", "147 Bugis Street, Singapore 188735", "PayPal", "Completed", new DateTime(2024, 2, 8, 9, 15, 0), new DateTime(2024, 2, 12, 10, 30, 0) },
                    { 8, 85.00m, "Delivered", "258 Holland Village, Singapore 275832", "Apple Pay", "Completed", new DateTime(2024, 2, 14, 14, 20, 0), new DateTime(2024, 2, 18, 16, 40, 0) },
                    { 9, 149.99m, "Delivered", "369 Tampines Mall, Singapore 529510", "Credit Card", "Completed", new DateTime(2024, 2, 20, 12, 10, 0), new DateTime(2024, 2, 24, 13, 25, 0) },
                    { 10, 120.00m, "Delivered", "741 Jurong Point, Singapore 648886", "PayPal", "Completed", new DateTime(2024, 2, 28, 15, 30, 0), new DateTime(2024, 3, 3, 14, 15, 0) },
                    
                    // March 2024 Orders
                    { 1, 340.00m, "Delivered", "456 Customer Road, Singapore", "Credit Card", "Completed", new DateTime(2024, 3, 5, 10, 45, 0), new DateTime(2024, 3, 9, 11, 20, 0) },
                    { 3, 160.00m, "Delivered", "789 Sentosa Island, Singapore 098765", "Apple Pay", "Completed", new DateTime(2024, 3, 12, 16, 30, 0), new DateTime(2024, 3, 16, 17, 45, 0) },
                    { 5, 70.00m, "Delivered", "654 Chinatown, Singapore 058357", "PayPal", "Completed", new DateTime(2024, 3, 18, 13, 15, 0), new DateTime(2024, 3, 22, 14, 30, 0) },
                    { 7, 100.00m, "Delivered", "147 Bugis Street, Singapore 188735", "Credit Card", "Completed", new DateTime(2024, 3, 25, 11, 40, 0), new DateTime(2024, 3, 29, 12, 55, 0) },
                    
                    // April 2024 Orders
                    { 2, 259.00m, "Delivered", "789 Shopper Lane, Singapore", "Apple Pay", "Completed", new DateTime(2024, 4, 2, 14, 20, 0), new DateTime(2024, 4, 6, 15, 35, 0) },
                    { 4, 85.00m, "Delivered", "321 Clarke Quay, Singapore 179024", "PayPal", "Completed", new DateTime(2024, 4, 8, 9, 30, 0), new DateTime(2024, 4, 12, 10, 45, 0) },
                    { 6, 130.00m, "Delivered", "987 Little India, Singapore 209695", "Credit Card", "Completed", new DateTime(2024, 4, 15, 12, 10, 0), new DateTime(2024, 4, 19, 13, 25, 0) },
                    { 8, 180.00m, "Delivered", "258 Holland Village, Singapore 275832", "Apple Pay", "Completed", new DateTime(2024, 4, 22, 16, 45, 0), new DateTime(2024, 4, 26, 17, 20, 0) },
                    { 10, 75.00m, "Delivered", "741 Jurong Point, Singapore 648886", "PayPal", "Completed", new DateTime(2024, 4, 28, 14, 15, 0), new DateTime(2024, 5, 2, 15, 30, 0) },
                    
                    // May 2024 Orders
                    { 1, 300.00m, "Delivered", "456 Customer Road, Singapore", "Credit Card", "Completed", new DateTime(2024, 5, 3, 11, 30, 0), new DateTime(2024, 5, 7, 12, 45, 0) },
                    { 3, 120.00m, "Delivered", "789 Sentosa Island, Singapore 098765", "PayPal", "Completed", new DateTime(2024, 5, 10, 15, 20, 0), new DateTime(2024, 5, 14, 16, 35, 0) },
                    { 5, 149.99m, "Delivered", "654 Chinatown, Singapore 058357", "Apple Pay", "Completed", new DateTime(2024, 5, 17, 13, 40, 0), new DateTime(2024, 5, 21, 14, 55, 0) },
                    { 7, 90.00m, "Delivered", "147 Bugis Street, Singapore 188735", "Credit Card", "Completed", new DateTime(2024, 5, 24, 10, 25, 0), new DateTime(2024, 5, 28, 11, 40, 0) },
                    { 9, 170.00m, "Delivered", "369 Tampines Mall, Singapore 529510", "PayPal", "Completed", new DateTime(2024, 5, 30, 16, 50, 0), new DateTime(2024, 6, 3, 17, 15, 0) },
                    
                    // June 2024 Orders
                    { 2, 160.00m, "Delivered", "789 Shopper Lane, Singapore", "Apple Pay", "Completed", new DateTime(2024, 6, 5, 12, 15, 0), new DateTime(2024, 6, 9, 13, 30, 0) },
                    { 4, 85.00m, "Delivered", "321 Clarke Quay, Singapore 179024", "Credit Card", "Completed", new DateTime(2024, 6, 12, 14, 40, 0), new DateTime(2024, 6, 16, 15, 55, 0) },
                    { 6, 100.00m, "Delivered", "987 Little India, Singapore 209695", "PayPal", "Completed", new DateTime(2024, 6, 18, 11, 20, 0), new DateTime(2024, 6, 22, 12, 35, 0) },
                    { 8, 259.00m, "Delivered", "258 Holland Village, Singapore 275832", "Apple Pay", "Completed", new DateTime(2024, 6, 25, 15, 30, 0), new DateTime(2024, 6, 29, 16, 45, 0) },
                    { 10, 70.00m, "Delivered", "741 Jurong Point, Singapore 648886", "Credit Card", "Completed", new DateTime(2024, 6, 28, 13, 10, 0), new DateTime(2024, 7, 2, 14, 25, 0) }
                });

            // Continue with more orders for July-December 2024
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "CustomerId", "TotalAmount", "Status", "Address", "PaymentMethod", "PaymentStatus", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    // July 2024 Orders
                    { 1, 370.00m, "Delivered", "456 Customer Road, Singapore", "PayPal", "Completed", new DateTime(2024, 7, 8, 10, 30, 0), new DateTime(2024, 7, 12, 11, 45, 0) },
                    { 3, 180.00m, "Delivered", "789 Sentosa Island, Singapore 098765", "Credit Card", "Completed", new DateTime(2024, 7, 15, 14, 20, 0), new DateTime(2024, 7, 19, 15, 35, 0) },
                    { 5, 120.00m, "Delivered", "654 Chinatown, Singapore 058357", "Apple Pay", "Completed", new DateTime(2024, 7, 22, 16, 45, 0), new DateTime(2024, 7, 26, 17, 20, 0) },
                    { 7, 149.99m, "Delivered", "147 Bugis Street, Singapore 188735", "PayPal", "Completed", new DateTime(2024, 7, 28, 12, 15, 0), new DateTime(2024, 8, 1, 13, 30, 0) },
                    
                    // August 2024 Orders
                    { 2, 85.00m, "Delivered", "789 Shopper Lane, Singapore", "Credit Card", "Completed", new DateTime(2024, 8, 3, 11, 40, 0), new DateTime(2024, 8, 7, 12, 55, 0) },
                    { 4, 259.00m, "Delivered", "321 Clarke Quay, Singapore 179024", "Apple Pay", "Completed", new DateTime(2024, 8, 10, 15, 25, 0), new DateTime(2024, 8, 14, 16, 40, 0) },
                    { 6, 130.00m, "Delivered", "987 Little India, Singapore 209695", "PayPal", "Completed", new DateTime(2024, 8, 17, 13, 30, 0), new DateTime(2024, 8, 21, 14, 45, 0) },
                    { 8, 90.00m, "Delivered", "258 Holland Village, Singapore 275832", "Credit Card", "Completed", new DateTime(2024, 8, 24, 10, 50, 0), new DateTime(2024, 8, 28, 11, 15, 0) },
                    { 9, 170.00m, "Delivered", "369 Tampines Mall, Singapore 529510", "Apple Pay", "Completed", new DateTime(2024, 8, 30, 16, 20, 0), new DateTime(2024, 9, 3, 17, 35, 0) },
                    
                    // September 2024 Orders
                    { 10, 160.00m, "Delivered", "741 Jurong Point, Singapore 648886", "PayPal", "Completed", new DateTime(2024, 9, 5, 14, 10, 0), new DateTime(2024, 9, 9, 15, 25, 0) },
                    { 1, 100.00m, "Delivered", "456 Customer Road, Singapore", "Credit Card", "Completed", new DateTime(2024, 9, 12, 12, 30, 0), new DateTime(2024, 9, 16, 13, 45, 0) },
                    { 3, 85.00m, "Delivered", "789 Sentosa Island, Singapore 098765", "Apple Pay", "Completed", new DateTime(2024, 9, 18, 11, 15, 0), new DateTime(2024, 9, 22, 12, 30, 0) },
                    { 5, 180.00m, "Delivered", "654 Chinatown, Singapore 058357", "PayPal", "Completed", new DateTime(2024, 9, 25, 15, 40, 0), new DateTime(2024, 9, 29, 16, 55, 0) },
                    { 7, 75.00m, "Delivered", "147 Bugis Street, Singapore 188735", "Credit Card", "Completed", new DateTime(2024, 9, 28, 13, 25, 0), new DateTime(2024, 10, 2, 14, 40, 0) },
                    
                    // October 2024 Orders
                    { 2, 259.00m, "Delivered", "789 Shopper Lane, Singapore", "Apple Pay", "Completed", new DateTime(2024, 10, 4, 10, 20, 0), new DateTime(2024, 10, 8, 11, 35, 0) },
                    { 4, 120.00m, "Delivered", "321 Clarke Quay, Singapore 179024", "PayPal", "Completed", new DateTime(2024, 10, 11, 14, 45, 0), new DateTime(2024, 10, 15, 15, 20, 0) },
                    { 6, 149.99m, "Delivered", "987 Little India, Singapore 209695", "Credit Card", "Completed", new DateTime(2024, 10, 18, 16, 30, 0), new DateTime(2024, 10, 22, 17, 45, 0) },
                    { 8, 70.00m, "Delivered", "258 Holland Village, Singapore 275832", "Apple Pay", "Completed", new DateTime(2024, 10, 25, 12, 50, 0), new DateTime(2024, 10, 29, 13, 15, 0) },
                    { 9, 160.00m, "Delivered", "369 Tampines Mall, Singapore 529510", "PayPal", "Completed", new DateTime(2024, 10, 30, 15, 35, 0), new DateTime(2024, 11, 3, 16, 50, 0) },
                    
                    // November 2024 Orders
                    { 10, 85.00m, "Delivered", "741 Jurong Point, Singapore 648886", "Credit Card", "Completed", new DateTime(2024, 11, 6, 11, 25, 0), new DateTime(2024, 11, 10, 12, 40, 0) },
                    { 1, 180.00m, "Delivered", "456 Customer Road, Singapore", "Apple Pay", "Completed", new DateTime(2024, 11, 13, 13, 20, 0), new DateTime(2024, 11, 17, 14, 35, 0) },
                    { 3, 100.00m, "Delivered", "789 Sentosa Island, Singapore 098765", "PayPal", "Completed", new DateTime(2024, 11, 20, 16, 40, 0), new DateTime(2024, 11, 24, 17, 55, 0) },
                    { 5, 259.00m, "Delivered", "654 Chinatown, Singapore 058357", "Credit Card", "Completed", new DateTime(2024, 11, 27, 14, 15, 0), new DateTime(2024, 12, 1, 15, 30, 0) },
                    
                    // December 2024 Orders
                    { 7, 90.00m, "Delivered", "147 Bugis Street, Singapore 188735", "Apple Pay", "Completed", new DateTime(2024, 12, 3, 10, 45, 0), new DateTime(2024, 12, 7, 11, 20, 0) },
                    { 2, 170.00m, "Delivered", "789 Shopper Lane, Singapore", "PayPal", "Completed", new DateTime(2024, 12, 10, 15, 30, 0), new DateTime(2024, 12, 14, 16, 45, 0) },
                    { 4, 120.00m, "Delivered", "321 Clarke Quay, Singapore 179024", "Credit Card", "Completed", new DateTime(2024, 12, 17, 12, 25, 0), new DateTime(2024, 12, 21, 13, 40, 0) },
                    { 6, 85.00m, "Delivered", "987 Little India, Singapore 209695", "Apple Pay", "Completed", new DateTime(2024, 12, 24, 14, 50, 0), new DateTime(2024, 12, 28, 15, 15, 0) },
                    { 8, 149.99m, "Delivered", "258 Holland Village, Singapore 275832", "PayPal", "Completed", new DateTime(2024, 12, 30, 16, 35, 0), new DateTime(2025, 1, 3, 17, 50, 0) }
                });

            // 2025 Orders (January - August)
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "CustomerId", "TotalAmount", "Status", "Address", "PaymentMethod", "PaymentStatus", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    // January 2025 Orders
                    { 9, 160.00m, "Delivered", "369 Tampines Mall, Singapore 529510", "Credit Card", "Completed", new DateTime(2025, 1, 4, 11, 20, 0), new DateTime(2025, 1, 8, 12, 35, 0) },
                    { 10, 100.00m, "Delivered", "741 Jurong Point, Singapore 648886", "Apple Pay", "Completed", new DateTime(2025, 1, 11, 14, 45, 0), new DateTime(2025, 1, 15, 15, 20, 0) },
                    { 1, 259.00m, "Delivered", "456 Customer Road, Singapore", "PayPal", "Completed", new DateTime(2025, 1, 18, 16, 30, 0), new DateTime(2025, 1, 22, 17, 45, 0) },
                    { 3, 85.00m, "Delivered", "789 Sentosa Island, Singapore 098765", "Credit Card", "Completed", new DateTime(2025, 1, 25, 13, 15, 0), new DateTime(2025, 1, 29, 14, 30, 0) },
                    { 5, 180.00m, "Delivered", "654 Chinatown, Singapore 058357", "Apple Pay", "Completed", new DateTime(2025, 1, 30, 10, 40, 0), new DateTime(2025, 2, 3, 11, 55, 0) },
                    
                    // February 2025 Orders
                    { 7, 120.00m, "Delivered", "147 Bugis Street, Singapore 188735", "PayPal", "Completed", new DateTime(2025, 2, 5, 12, 25, 0), new DateTime(2025, 2, 9, 13, 40, 0) },
                    { 2, 170.00m, "Delivered", "789 Shopper Lane, Singapore", "Credit Card", "Completed", new DateTime(2025, 2, 12, 15, 50, 0), new DateTime(2025, 2, 16, 16, 15, 0) },
                    { 4, 149.99m, "Delivered", "321 Clarke Quay, Singapore 179024", "Apple Pay", "Completed", new DateTime(2025, 2, 19, 11, 35, 0), new DateTime(2025, 2, 23, 12, 50, 0) },
                    { 6, 90.00m, "Delivered", "987 Little India, Singapore 209695", "PayPal", "Completed", new DateTime(2025, 2, 26, 14, 20, 0), new DateTime(2025, 3, 2, 15, 35, 0) },
                    { 8, 75.00m, "Delivered", "258 Holland Village, Singapore 275832", "Credit Card", "Completed", new DateTime(2025, 2, 28, 16, 45, 0), new DateTime(2025, 3, 4, 17, 20, 0) },
                    
                    // March 2025 Orders
                    { 9, 259.00m, "Delivered", "369 Tampines Mall, Singapore 529510", "Apple Pay", "Completed", new DateTime(2025, 3, 6, 10, 30, 0), new DateTime(2025, 3, 10, 11, 45, 0) },
                    { 10, 130.00m, "Delivered", "741 Jurong Point, Singapore 648886", "PayPal", "Completed", new DateTime(2025, 3, 13, 13, 55, 0), new DateTime(2025, 3, 17, 14, 20, 0) },
                    { 1, 85.00m, "Delivered", "456 Customer Road, Singapore", "Credit Card", "Completed", new DateTime(2025, 3, 20, 12, 10, 0), new DateTime(2025, 3, 24, 13, 25, 0) },
                    { 3, 160.00m, "Delivered", "789 Sentosa Island, Singapore 098765", "Apple Pay", "Completed", new DateTime(2025, 3, 27, 15, 40, 0), new DateTime(2025, 3, 31, 16, 55, 0) },
                    
                    // April 2025 Orders
                    { 5, 100.00m, "Delivered", "654 Chinatown, Singapore 058357", "PayPal", "Completed", new DateTime(2025, 4, 3, 11, 15, 0), new DateTime(2025, 4, 7, 12, 30, 0) },
                    { 7, 180.00m, "Delivered", "147 Bugis Street, Singapore 188735", "Credit Card", "Completed", new DateTime(2025, 4, 10, 14, 25, 0), new DateTime(2025, 4, 14, 15, 40, 0) },
                    { 2, 120.00m, "Delivered", "789 Shopper Lane, Singapore", "Apple Pay", "Completed", new DateTime(2025, 4, 17, 16, 50, 0), new DateTime(2025, 4, 21, 17, 15, 0) },
                    { 4, 70.00m, "Delivered", "321 Clarke Quay, Singapore 179024", "PayPal", "Completed", new DateTime(2025, 4, 24, 13, 35, 0), new DateTime(2025, 4, 28, 14, 50, 0) },
                    { 6, 259.00m, "Delivered", "987 Little India, Singapore 209695", "Credit Card", "Completed", new DateTime(2025, 4, 30, 10, 20, 0), new DateTime(2025, 5, 4, 11, 35, 0) },
                    
                    // May 2025 Orders
                    { 8, 149.99m, "Delivered", "258 Holland Village, Singapore 275832", "Apple Pay", "Completed", new DateTime(2025, 5, 7, 12, 45, 0), new DateTime(2025, 5, 11, 13, 20, 0) },
                    { 9, 85.00m, "Delivered", "369 Tampines Mall, Singapore 529510", "PayPal", "Completed", new DateTime(2025, 5, 14, 15, 30, 0), new DateTime(2025, 5, 18, 16, 45, 0) },
                    { 10, 170.00m, "Delivered", "741 Jurong Point, Singapore 648886", "Credit Card", "Completed", new DateTime(2025, 5, 21, 11, 55, 0), new DateTime(2025, 5, 25, 12, 20, 0) },
                    { 1, 90.00m, "Delivered", "456 Customer Road, Singapore", "Apple Pay", "Completed", new DateTime(2025, 5, 28, 14, 40, 0), new DateTime(2025, 6, 1, 15, 55, 0) },
                    
                    // June 2025 Orders
                    { 3, 130.00m, "Delivered", "789 Sentosa Island, Singapore 098765", "PayPal", "Completed", new DateTime(2025, 6, 4, 16, 25, 0), new DateTime(2025, 6, 8, 17, 40, 0) },
                    { 5, 180.00m, "Delivered", "654 Chinatown, Singapore 058357", "Credit Card", "Completed", new DateTime(2025, 6, 11, 13, 50, 0), new DateTime(2025, 6, 15, 14, 15, 0) },
                    { 7, 75.00m, "Delivered", "147 Bugis Street, Singapore 188735", "Apple Pay", "Completed", new DateTime(2025, 6, 18, 10, 35, 0), new DateTime(2025, 6, 22, 11, 50, 0) },
                    { 2, 259.00m, "Delivered", "789 Shopper Lane, Singapore", "PayPal", "Completed", new DateTime(2025, 6, 25, 12, 20, 0), new DateTime(2025, 6, 29, 13, 35, 0) },
                    { 4, 100.00m, "Delivered", "321 Clarke Quay, Singapore 179024", "Credit Card", "Completed", new DateTime(2025, 6, 30, 15, 45, 0), new DateTime(2025, 7, 4, 16, 20, 0) },
                    
                    // July 2025 Orders
                    { 6, 120.00m, "Delivered", "987 Little India, Singapore 209695", "Apple Pay", "Completed", new DateTime(2025, 7, 7, 11, 30, 0), new DateTime(2025, 7, 11, 12, 45, 0) },
                    { 8, 85.00m, "Delivered", "258 Holland Village, Singapore 275832", "PayPal", "Completed", new DateTime(2025, 7, 14, 14, 55, 0), new DateTime(2025, 7, 18, 15, 20, 0) },
                    { 9, 160.00m, "Delivered", "369 Tampines Mall, Singapore 529510", "Credit Card", "Completed", new DateTime(2025, 7, 21, 16, 40, 0), new DateTime(2025, 7, 25, 17, 55, 0) },
                    { 10, 149.99m, "Delivered", "741 Jurong Point, Singapore 648886", "Apple Pay", "Completed", new DateTime(2025, 7, 28, 13, 25, 0), new DateTime(2025, 8, 1, 14, 40, 0) },
                    
                    // August 2025 Orders (including some pending, shipped, and cancelled)
                    { 1, 180.00m, "Delivered", "456 Customer Road, Singapore", "PayPal", "Completed", new DateTime(2025, 8, 2, 10, 15, 0), new DateTime(2025, 8, 6, 11, 30, 0) },
                    { 3, 90.00m, "Shipped", "789 Sentosa Island, Singapore 098765", "Credit Card", "Completed", new DateTime(2025, 8, 5, 12, 40, 0), new DateTime(2025, 8, 5, 12, 40, 0) },
                    { 5, 259.00m, "Shipped", "654 Chinatown, Singapore 058357", "Apple Pay", "Completed", new DateTime(2025, 8, 8, 15, 20, 0), new DateTime(2025, 8, 8, 15, 20, 0) },
                    { 7, 130.00m, "Pending", "147 Bugis Street, Singapore 188735", "PayPal", "Pending", new DateTime(2025, 8, 12, 14, 30, 0), new DateTime(2025, 8, 12, 14, 30, 0) },
                    { 2, 170.00m, "Pending", "789 Shopper Lane, Singapore", "Credit Card", "Pending", new DateTime(2025, 8, 15, 16, 45, 0), new DateTime(2025, 8, 15, 16, 45, 0) },
                    { 4, 85.00m, "Cancelled", "321 Clarke Quay, Singapore 179024", "Apple Pay", "Refunded", new DateTime(2025, 8, 18, 11, 25, 0), new DateTime(2025, 8, 19, 10, 15, 0) },
                    { 6, 120.00m, "Pending", "987 Little India, Singapore 209695", "PayPal", "Pending", new DateTime(2025, 8, 22, 13, 50, 0), new DateTime(2025, 8, 22, 13, 50, 0) },
                    { 8, 100.00m, "Shipped", "258 Holland Village, Singapore 275832", "Credit Card", "Completed", new DateTime(2025, 8, 25, 12, 35, 0), new DateTime(2025, 8, 25, 12, 35, 0) },
                    { 9, 149.99m, "Pending", "369 Tampines Mall, Singapore 529510", "Apple Pay", "Pending", new DateTime(2025, 8, 28, 15, 40, 0), new DateTime(2025, 8, 28, 15, 40, 0) },
                    { 10, 75.00m, "Cancelled", "741 Jurong Point, Singapore 648886", "PayPal", "Refunded", new DateTime(2025, 8, 30, 17, 20, 0), new DateTime(2025, 8, 31, 9, 45, 0) }
                });

            // Insert corresponding OrderItems for orders 8-50 (skipping the first 7 which already exist)
            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderId", "ProductVariantId", "ProductName", "Size", "Quantity", "UnitPrice", "TotalPrice" },
                values: new object[,]
                {
                    // Order 8 (January 2024) - Customer 3, $259.00
                    { 8, 1, "Cloudsurfer Next", 8, 1, 259.00m, 259.00m },
                    
                    // Order 9 (January 2024) - Customer 4, $150.00
                    { 9, 6, "Aero Burst", 8, 1, 150.00m, 150.00m },
                    
                    // Order 10 (January 2024) - Customer 5, $170.00
                    { 10, 49, "Air Jordan 1", 8, 1, 170.00m, 170.00m },
                    
                    // Order 11 (January 2024) - Customer 2, $320.00
                    { 11, 34, "Nike Zoom Vomero S", 8, 2, 160.00m, 320.00m },
                    
                    // Order 12 (February 2024) - Customer 6, $90.00
                    { 12, 64, "Air Force 1", 8, 1, 90.00m, 90.00m },
                    
                    // Order 13 (February 2024) - Customer 7, $180.00
                    { 13, 51, "Ultraboost 22", 8, 1, 180.00m, 180.00m },
                    
                    // Order 14 (February 2024) - Customer 8, $85.00
                    { 14, 66, "Superstar", 8, 1, 85.00m, 85.00m },
                    
                    // Order 15 (February 2024) - Customer 9, $149.99
                    { 15, 23, "Nike Max 260", 10, 1, 149.99m, 149.99m },
                    
                    // Order 16 (February 2024) - Customer 10, $120.00
                    { 16, 16, "GoodShoe 0.2", 9, 1, 120.00m, 120.00m },
                    
                    // Order 17 (March 2024) - Customer 1, $340.00
                    { 17, 49, "Air Jordan 1", 8, 2, 170.00m, 340.00m },
                    
                    // Order 18 (March 2024) - Customer 3, $160.00
                    { 18, 34, "Nike Zoom Vomero S", 8, 1, 160.00m, 160.00m },
                    
                    // Order 19 (March 2024) - Customer 5, $70.00
                    { 19, 80, "Club C 85", 8, 1, 75.00m, 75.00m },
                    
                    // Order 20 (March 2024) - Customer 7, $100.00
                    { 20, 94, "Blazer Mid", 8, 1, 100.00m, 100.00m },
                    
                    // Order 21 (April 2024) - Customer 2, $259.00
                    { 21, 1, "Cloudsurfer Next", 8, 1, 259.00m, 259.00m },
                    
                    // Order 22 (April 2024) - Customer 4, $85.00
                    { 22, 41, "NY 90 Shoes", 8, 1, 85.00m, 85.00m },
                    
                    // Order 23 (April 2024) - Customer 6, $130.00
                    { 23, 87, "React Element 55", 8, 1, 130.00m, 130.00m },
                    
                    // Order 24 (April 2024) - Customer 8, $180.00
                    { 24, 51, "Ultraboost 22", 8, 1, 180.00m, 180.00m },
                    
                    // Order 25 (April 2024) - Customer 10, $75.00
                    { 25, 80, "Club C 85", 8, 1, 75.00m, 75.00m },
                    
                    // Order 26 (May 2024) - Customer 1, $300.00
                    { 26, 1, "Cloudsurfer Next", 8, 1, 259.00m, 259.00m },
                    { 26, 41, "NY 90 Shoes", 8, 1, 85.00m, 85.00m },
                    
                    // Order 27 (May 2024) - Customer 3, $120.00
                    { 27, 16, "GoodShoe 0.2", 9, 1, 120.00m, 120.00m },
                    
                    // Order 28 (May 2024) - Customer 5, $149.99
                    { 28, 23, "Nike Max 260", 10, 1, 149.99m, 149.99m },
                    
                    // Order 29 (May 2024) - Customer 7, $90.00
                    { 29, 64, "Air Force 1", 8, 1, 90.00m, 90.00m },
                    
                    // Order 30 (May 2024) - Customer 9, $170.00
                    { 30, 49, "Air Jordan 1", 8, 1, 170.00m, 170.00m },
                    
                    // Order 31 (June 2024) - Customer 2, $160.00
                    { 31, 34, "Nike Zoom Vomero S", 8, 1, 160.00m, 160.00m },
                    
                    // Order 32 (June 2024) - Customer 4, $85.00
                    { 32, 66, "Superstar", 8, 1, 85.00m, 85.00m },
                    
                    // Order 33 (June 2024) - Customer 6, $100.00
                    { 33, 94, "Blazer Mid", 8, 1, 100.00m, 100.00m },
                    
                    // Order 34 (June 2024) - Customer 8, $259.00
                    { 34, 1, "Cloudsurfer Next", 8, 1, 259.00m, 259.00m },
                    
                    // Order 35 (June 2024) - Customer 10, $70.00
                    { 35, 57, "Classic Leather", 8, 1, 70.00m, 70.00m },
                    
                    // Order 36 (July 2024) - Customer 1, $370.00
                    { 36, 49, "Air Jordan 1", 8, 1, 170.00m, 170.00m },
                    { 36, 1, "Cloudsurfer Next", 10, 1, 259.00m, 259.00m },
                    
                    // Order 37 (July 2024) - Customer 3, $180.00
                    { 37, 51, "Ultraboost 22", 8, 1, 180.00m, 180.00m },
                    
                    // Order 38 (July 2024) - Customer 5, $120.00
                    { 38, 16, "GoodShoe 0.2", 9, 1, 120.00m, 120.00m },
                    
                    // Order 39 (July 2024) - Customer 7, $149.99
                    { 39, 23, "Nike Max 260", 10, 1, 149.99m, 149.99m },
                    
                    // Order 40 (August 2024) - Customer 2, $85.00
                    { 40, 41, "NY 90 Shoes", 8, 1, 85.00m, 85.00m },
                    
                    // Order 41 (August 2024) - Customer 4, $259.00
                    { 41, 1, "Cloudsurfer Next", 8, 1, 259.00m, 259.00m },
                    
                    // Order 42 (August 2024) - Customer 6, $130.00
                    { 42, 87, "React Element 55", 8, 1, 130.00m, 130.00m },
                    
                    // Order 43 (August 2024) - Customer 8, $90.00
                    { 43, 64, "Air Force 1", 8, 1, 90.00m, 90.00m },
                    
                    // Order 44 (August 2024) - Customer 9, $170.00
                    { 44, 49, "Air Jordan 1", 8, 1, 170.00m, 170.00m },
                    
                    // Order 45 (September 2024) - Customer 10, $160.00
                    { 45, 34, "Nike Zoom Vomero S", 8, 1, 160.00m, 160.00m },
                    
                    // Order 46 (September 2024) - Customer 1, $100.00
                    { 46, 10, "GoodShoe 0.1", 10, 1, 100.00m, 100.00m },
                    
                    // Order 47 (September 2024) - Customer 3, $85.00
                    { 47, 66, "Superstar", 8, 1, 85.00m, 85.00m },
                    
                    // Order 48 (September 2024) - Customer 5, $180.00
                    { 48, 51, "Ultraboost 22", 8, 1, 180.00m, 180.00m },
                    
                    // Order 49 (September 2024) - Customer 7, $75.00
                    { 49, 80, "Club C 85", 8, 1, 75.00m, 75.00m },
                    
                    // Order 50 (October 2024) - Customer 2, $259.00
                    { 50, 1, "Cloudsurfer Next", 8, 1, 259.00m, 259.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete OrderItems first (due to foreign key constraints)
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValues: new object[] { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 });

            // Delete Orders (OrderIds 8 through 89)
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValues: new object[] { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89 });
        }
    }
}