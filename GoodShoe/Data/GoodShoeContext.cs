using GoodShoe.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoodShoe.Data
{
    public class GoodShoeDbContext : IdentityDbContext<ApplicationUser>
    {
        public GoodShoeDbContext(DbContextOptions<GoodShoeDbContext> options) : base(options)
        {
        }
        
        public DbSet<Product> Product { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Purchase> Purchases { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Location).HasMaxLength(255);
            });


            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Brand).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.Color).IsRequired();
                entity.Property(e => e.Category).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ImageUrl).HasMaxLength(200);
                entity.Property(e => e.AvailableSizes).IsRequired();
            });

            // Customer configuration - matching migration with FirstName/LastName
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerID);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Address).HasMaxLength(500);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasIndex(e => e.Email);

                // One-to-one with Cart
                entity.HasOne(e => e.Cart)
                      .WithOne(e => e.Customer)
                      .HasForeignKey<Cart>(e => e.CustomerID);
            });

            // Cart configuration
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.HasIndex(e => e.CustomerID).IsUnique();
            });

            // CartItem configuration
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Size).IsRequired().HasMaxLength(10);

                entity.HasOne(e => e.Cart)
                      .WithMany(e => e.CartItems)
                      .HasForeignKey(e => e.CartId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                      .WithMany(e => e.CartItems)
                      .HasForeignKey(e => e.ProductID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Order configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderID);
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(500);
                entity.Property(e => e.PaymentMethod).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Customer)
                      .WithMany(e => e.Orders)
                      .HasForeignKey(e => e.CustomerID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.CustomerID);
                entity.HasIndex(e => e.Status);
            });

            // OrderItem configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Size).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Order)
                      .WithMany(e => e.OrderItems)
                      .HasForeignKey(e => e.OrderID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                      .WithMany(e => e.OrderItems)
                      .HasForeignKey(e => e.ProductID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Admin configuration (single record)
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AdminID);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Currency).IsRequired().HasMaxLength(10).HasDefaultValue("SGD");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Seed data - matching your migration exactly
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Update existing products with new structure
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Cloudsurfer Next", Brand = "Puma", Price = 259.00m, Description = "Lace up in Swiss-engineered runners with these Cloudsurfer Next trainers from On Running.", StockCount = 3, Color = "White", Category = "Unisex", ImageUrl = "/images/products/image1.png", AvailableSizes = "8,9,10,11,12" },
                new Product { Id = 2, Name = "Aero Burst", Brand = "Sketchers", Price = 150.00m, Description = "Hit every mile marker in long-distance confidence and premium cushioned comfort with Skechers Aero Burst™.", StockCount = 7, Color = "Periwinkle", Category = "Women", ImageUrl = "/images/products/image2.png", AvailableSizes = "8,9,10,11" },
                new Product { Id = 3, Name = "GoodShoe 0.1", Brand = "GoodShoe", Price = 100.00m, Description = "Men's Shoes", StockCount = 5, Color = "Brown", Category = "Men", ImageUrl = "/images/products/image3.png", AvailableSizes = "10,11,12,13,14" },
                new Product { Id = 4, Name = "GoodShoe 0.2", Brand = "GoodShoe", Price = 120.00m, Description = "Updated Women's Shoes", StockCount = 8, Color = "Brown", Category = "Women", ImageUrl = "/images/products/image4.png", AvailableSizes = "8,9,10,11,12" },
                new Product { Id = 5, Name = "GoodShoe 0.3", Brand = "GoodShoe", Price = 110.00m, Description = "New Unisex Shoes", StockCount = 6, Color = "Blue", Category = "Unisex", ImageUrl = "/images/products/image5.png", AvailableSizes = "9,10,11,12,13" },
                new Product { Id = 6, Name = "Nike Max 260", Brand = "Nike", Price = 149.99m, Description = "The Nike Air Max 270 delivers visible cushioning under every step.", StockCount = 15, Color = "White", Category = "Men", ImageUrl = "/images/products/image6.png", AvailableSizes = "10,11,12,13,14,15" }
            );

            // Seed customers - using STATIC dates instead of DateTime.Now
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerID = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@email.com", Phone = "+65 9876 5432", Address = "456 Customer Road, Singapore", CreatedAt = new DateTime(2024, 1, 15), UpdatedAt = new DateTime(2024, 1, 15) },
                new Customer { CustomerID = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@email.com", Phone = "+65 8765 4321", Address = "789 Shopper Lane, Singapore", CreatedAt = new DateTime(2024, 1, 16), UpdatedAt = new DateTime(2024, 1, 16) }
            );

            // Seed orders - using STATIC dates
            modelBuilder.Entity<Order>().HasData(
                new Order { OrderID = 1, CustomerID = 1, TotalAmount = 409.00m, Status = "Pending", Address = "456 Customer Road, Singapore", PaymentMethod = "Credit Card", CreatedAt = new DateTime(2024, 7, 4) },
                new Order { OrderID = 2, CustomerID = 2, TotalAmount = 150.00m, Status = "Shipped", Address = "789 Shopper Lane, Singapore", PaymentMethod = "PayPal", CreatedAt = new DateTime(2024, 7, 5) },
                new Order { OrderID = 3, CustomerID = 1, TotalAmount = 259.00m, Status = "Delivered", Address = "456 Customer Road, Singapore", PaymentMethod = "Credit Card", CreatedAt = new DateTime(2024, 7, 1) }
            );

            // Seed order items
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, OrderID = 1, ProductID = 1, ProductName = "Cloudsurfer Next", Size = "10", Quantity = 1, Price = 259.00m },
                new OrderItem { Id = 2, OrderID = 1, ProductID = 2, ProductName = "Aero Burst", Size = "9", Quantity = 1, Price = 150.00m },
                new OrderItem { Id = 3, OrderID = 2, ProductID = 2, ProductName = "Aero Burst", Size = "10", Quantity = 1, Price = 150.00m },
                new OrderItem { Id = 4, OrderID = 3, ProductID = 1, ProductName = "Cloudsurfer Next", Size = "11", Quantity = 1, Price = 259.00m }
            );

            // Seed single admin - using STATIC dates
            modelBuilder.Entity<Admin>().HasData(
                new Admin { AdminID = 1, UserName = "Admin User", Phone = "+65 1234 5678", DOB = new DateTime(1990, 1, 1), Email = "admin@goodshoe.com", Currency = "SGD", UpdatedAt = new DateTime(2024, 1, 1) }
            );
        }
    }
}