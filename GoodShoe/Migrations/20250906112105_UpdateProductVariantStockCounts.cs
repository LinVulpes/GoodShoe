using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodShoe.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductVariantStockCounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update ProductVariant stock counts to have more realistic inventory levels
            
            // Product 1 - Cloudsurfer Next (Puma) - Premium product, moderate stock
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 1, 8 },
                column: "StockCount",
                value: 15);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 1, 9 },
                column: "StockCount",
                value: 12);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 1, 10 },
                column: "StockCount",
                value: 18);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 1, 11 },
                column: "StockCount",
                value: 14);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 1, 12 },
                column: "StockCount",
                value: 10);

            // Product 2 - Aero Burst (Sketchers) - Popular model, higher stock
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 2, 8 },
                column: "StockCount",
                value: 25);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 2, 9 },
                column: "StockCount",
                value: 30);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 2, 10 },
                column: "StockCount",
                value: 28);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 2, 11 },
                column: "StockCount",
                value: 22);

            // Add missing size 12 for Product 2
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 2, 12 },
                column: "StockCount",
                value: 20);

            // Product 6 - Nike Max 260 - Reduce some stock as it's popular
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 6, 14 },
                column: "StockCount",
                value: 8); // Reduced from 15

            // Product 7 - Nike Free Metcon 6 - Update stock
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 7, 12 },
                column: "StockCount",
                value: 5); // Increased from 1

            // Product 8 - Nike Zoom Vomero S - Adjust stock levels
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 8, 8 },
                column: "StockCount",
                value: 8); // Reduced from 10

            // Product 18 - React Element 55 - Reduce stock for size 8
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 18, 8 },
                column: "StockCount",
                value: 6); // Reduced from 10

            // Product 20 - Continental 80 - Increase some low stock items
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 20, 8 },
                column: "StockCount",
                value: 5); // Increased from 1
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 20, 10 },
                column: "StockCount",
                value: 8); // Increased from 3
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 20, 12 },
                column: "StockCount",
                value: 7); // Increased from 3

            // Set some items as out of stock to create realistic scenarios
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 20, 15 },
                column: "StockCount",
                value: 0); // Keep as out of stock
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert stock counts to original values
            
            // Product 1 - Revert to original values
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 1, 8 },
                column: "StockCount",
                value: 7);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 1, 9 },
                column: "StockCount",
                value: 3);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 1, 10 },
                column: "StockCount",
                value: 3);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 1, 11 },
                column: "StockCount",
                value: 3);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 1, 12 },
                column: "StockCount",
                value: 3);

            // Product 2 - Revert to original values
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 2, 8 },
                column: "StockCount",
                value: 4);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 2, 9 },
                column: "StockCount",
                value: 7);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 2, 10 },
                column: "StockCount",
                value: 7);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 2, 11 },
                column: "StockCount",
                value: 17);

            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 2, 12 },
                column: "StockCount",
                value: 6);

            // Revert other changes
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 6, 14 },
                column: "StockCount",
                value: 11);

            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 7, 12 },
                column: "StockCount",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 8, 8 },
                column: "StockCount",
                value: 10);

            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 18, 8 },
                column: "StockCount",
                value: 7);

            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 20, 8 },
                column: "StockCount",
                value: 1);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 20, 10 },
                column: "StockCount",
                value: 3);
            
            migrationBuilder.UpdateData(
                table: "ProductVariant",
                keyColumns: new[] { "ProductId", "Size" },
                keyValues: new object[] { 20, 12 },
                column: "StockCount",
                value: 3);
        }
    }
}