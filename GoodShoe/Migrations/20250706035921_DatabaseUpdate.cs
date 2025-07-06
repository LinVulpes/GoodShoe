using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GoodShoe.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update existing Product table
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Product",
                newName: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)");

            migrationBuilder.AddColumn<string>(
                name: "AvailableSizes",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // Create Customers table
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            // Create Carts table
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create CartItems table
            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create Orders table
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create OrderItems table
            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            // Create Admin table (single record)
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "SGD"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminID);
                });

            // Create indexes
            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerID",
                table: "Carts",
                column: "CustomerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductID",
                table: "CartItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Status",
                table: "Orders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderID",
                table: "OrderItems",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductID",
                table: "OrderItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_Email",
                table: "Admin",
                column: "Email",
                unique: true);

            // Update existing Product data - FIXED: Only update Category and AvailableSizes
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Category", "AvailableSizes" },
                values: new object[] { "Unisex", "8,9,10,11,12" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Category", "AvailableSizes" },
                values: new object[] { "Women", "8,9,10,11" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Category", "AvailableSizes" },
                values: new object[] { "Men", "10,11,12,13,14" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Category", "AvailableSizes" },
                values: new object[] { "Women", "8,9,10,11,12" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Category", "AvailableSizes" },
                values: new object[] { "Unisex", "9,10,11,12,13" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Category", "AvailableSizes" },
                values: new object[] { "Men", "10,11,12,13,14,15" });

            // FIXED: Insert sample customers using FirstName and LastName (not Name)
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "FirstName", "LastName", "Email", "Phone", "Address", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "John", "Doe", "john.doe@email.com", "+65 9876 5432", "456 Customer Road, Singapore", new DateTime(2024, 1, 15), new DateTime(2024, 1, 15) },
                    { 2, "Jane", "Smith", "jane.smith@email.com", "+65 8765 4321", "789 Shopper Lane, Singapore", new DateTime(2024, 1, 16), new DateTime(2024, 1, 16) }
                });

            // Insert sample orders
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "CustomerID", "TotalAmount", "Status", "Address", "PaymentMethod", "CreatedAt" },
                values: new object[,]
                {
                    { 1, 1, 409.00m, "Pending", "456 Customer Road, Singapore", "Credit Card", new DateTime(2024, 7, 4) },
                    { 2, 2, 150.00m, "Shipped", "789 Shopper Lane, Singapore", "PayPal", new DateTime(2024, 7, 5) },
                    { 3, 1, 259.00m, "Delivered", "456 Customer Road, Singapore", "Credit Card", new DateTime(2024, 7, 1) }
                });

            // Insert sample order items
            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderID", "ProductID", "ProductName", "Size", "Quantity", "Price" },
                values: new object[,]
                {
                    { 1, 1, 1, "Cloudsurfer Next", "10", 1, 259.00m },
                    { 2, 1, 2, "Aero Burst", "9", 1, 150.00m },
                    { 3, 2, 2, "Aero Burst", "10", 1, 150.00m },
                    { 4, 3, 1, "Cloudsurfer Next", "11", 1, 259.00m }
                });

            // Insert single admin record
            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "AdminID", "UserName", "Phone", "DOB", "Email", "Currency", "UpdatedAt" },
                values: new object[] { 1, "Admin User", "+65 1234 5678", new DateTime(1990, 1, 1), "admin@goodshoe.com", "SGD", new DateTime(2024, 1, 1) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop all new tables
            migrationBuilder.DropTable(name: "Admin");
            migrationBuilder.DropTable(name: "OrderItems");
            migrationBuilder.DropTable(name: "CartItems");
            migrationBuilder.DropTable(name: "Orders");
            migrationBuilder.DropTable(name: "Carts");
            migrationBuilder.DropTable(name: "Customers");

            // Revert Product table changes
            migrationBuilder.DropColumn(
                name: "AvailableSizes",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Product",
                newName: "Gender");

            // Add back the Size column
            migrationBuilder.AddColumn<decimal>(
                name: "Size",
                table: "Product",
                type: "decimal(3,1)",
                nullable: false,
                defaultValue: 0m);

            // Revert product data
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Gender", "Size" },
                values: new object[] { "Unisex", 7.0m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Gender", "Size" },
                values: new object[] { "Women", 6.5m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Gender", "Size" },
                values: new object[] { "Men", 8.0m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Gender", "Size" },
                values: new object[] { "Women", 8.0m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Gender", "Size" },
                values: new object[] { "Unisex", 9.0m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Gender", "Size" },
                values: new object[] { "Men", 9.0m });
        }
    }
}
