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
        public DbSet<ProductVariant> ProductVariant { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Admin> Admin { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // ADDED: ApplicationUser configuration
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Location).HasMaxLength(255);
            });

            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Brand).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Color).HasMaxLength(50);
                entity.Property(e => e.Category).HasMaxLength(50);
                entity.Property(e => e.ImageUrl).HasMaxLength(200);
            });
            
            // ProductVariant configuration - Added for Shoe Sizes
            modelBuilder.Entity<ProductVariant>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Size).IsRequired();
                entity.Property(e => e.StockCount).HasDefaultValue(0);

                entity.HasOne(e => e.Product)
                    .WithMany(e => e.ProductVariants)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Unique constraint: one size per product
                entity.HasIndex(e => new { e.ProductId, e.Size }).IsUnique();
            });

            // Customer configuration - matching migration with FirstName/LastName
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Address).HasMaxLength(500);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasIndex(e => e.Email);
            });

            // Cart configuration
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.CartId);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Customer)
                    .WithMany(e => e.Carts)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // CartItem configuration - Updated to use ProductVariant
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.AddedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Cart)
                    .WithMany(e => e.CartItems)
                    .HasForeignKey(e => e.CartId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ProductVariant)
                    .WithMany(e => e.CartItems)
                    .HasForeignKey(e => e.ProductVariantId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Order configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId);
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50).HasDefaultValue("Pending");
                entity.Property(e => e.Address).HasMaxLength(500);
                entity.Property(e => e.PaymentMethod).HasMaxLength(50);
                entity.Property(e => e.PaymentStatus).HasMaxLength(20).HasDefaultValue("Pending");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Customer)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.CustomerId);
                entity.HasIndex(e => e.Status);
            });

            // OrderItem configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Size).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Order)
                    .WithMany(e => e.OrderItems)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ProductVariant)
                    .WithMany(e => e.OrderItems)
                    .HasForeignKey(e => e.ProductVariantId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Admin configuration (single account)
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AdminId);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Currency).HasMaxLength(10).HasDefaultValue("SGD");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.UserName).IsUnique();
            });

            // Seed data - to match the migration
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Update existing products with new structure
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Cloudsurfer Next", Brand = "Puma", Price = 259.00m, Description = "Lace up in Swiss-engineered runners with these Cloudsurfer Next trainers from On Running.", Color = "White", Category = "Unisex", ImageUrl = "/images/products/image1.png", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },
                new Product { ProductId = 2, Name = "Aero Burst", Brand = "Sketchers", Price = 150.00m, Description = "Hit every mile marker in long-distance confidence and premium cushioned comfort with Skechers Aero Burst™.", Color = "Periwinkle", Category = "Women", ImageUrl = "/images/products/image2.png", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },
                new Product { ProductId = 3, Name = "GoodShoe 0.1", Brand = "GoodShoe", Price = 100.00m, Description = "Men's Shoes", Color = "Brown", Category = "Men", ImageUrl = "/images/products/image3.png", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },
                new Product { ProductId = 4, Name = "GoodShoe 0.2", Brand = "GoodShoe", Price = 120.00m, Description = "Updated Women's Shoes", Color = "Brown", Category = "Women", ImageUrl = "/images/products/image4.png", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },
                new Product { ProductId = 5, Name = "GoodShoe 0.3", Brand = "GoodShoe", Price = 110.00m, Description = "New Unisex Shoes", Color = "Blue", Category = "Unisex", ImageUrl = "/images/products/image5.png", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },
                new Product { ProductId = 6, Name = "Nike Max 260", Brand = "Nike", Price = 149.99m, Description = "The Nike Air Max 270 delivers visible cushioning under every step.", Color = "White", Category = "Men", ImageUrl = "/images/products/image6.png", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) }
            );
            
            // Seed ProductVariants for sizes 8-16
            var productVariants = new List<ProductVariant>();
            var sizes = new int[] { 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            int variantId = 1;

            for (int productId = 1; productId <= 6; productId++)
            {
                foreach (var size in sizes)
                {
                    productVariants.Add(new ProductVariant
                    {
                        Id = variantId++,
                        ProductId = productId,
                        Size = size,
                        StockCount = GetStockForProduct(productId, size)
                    });
                }
            }
            
            modelBuilder.Entity<ProductVariant>().HasData(productVariants);

            // Seed customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@email.com", Phone = "+65 9876 5432", Address = "456 Customer Road, Singapore", CreatedAt = new DateTime(2024, 1, 15), UpdatedAt = new DateTime(2024, 1, 15) },
                new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@email.com", Phone = "+65 8765 4321", Address = "789 Shopper Lane, Singapore", CreatedAt = new DateTime(2024, 1, 16), UpdatedAt = new DateTime(2024, 1, 16) }
            );

            // Seed orders
            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, CustomerId = 1, TotalAmount = 409.00m, Status = "Pending", Address = "456 Customer Road, Singapore", PaymentMethod = "Credit Card", PaymentStatus = "Pending", CreatedAt = new DateTime(2024, 7, 4), UpdatedAt = new DateTime(2024, 7, 4) },
                new Order { OrderId = 2, CustomerId = 2, TotalAmount = 150.00m, Status = "Shipped", Address = "789 Shopper Lane, Singapore", PaymentMethod = "PayPal", PaymentStatus = "Completed", CreatedAt = new DateTime(2024, 7, 5), UpdatedAt = new DateTime(2024, 7, 5) },
                new Order { OrderId = 3, CustomerId = 1, TotalAmount = 259.00m, Status = "Delivered", Address = "456 Customer Road, Singapore", PaymentMethod = "Credit Card", PaymentStatus = "Completed", CreatedAt = new DateTime(2024, 7, 1), UpdatedAt = new DateTime(2024, 7, 1) }
            );

            // Seed order items
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, OrderId = 1, ProductVariantId = 3, ProductName = "Cloudsurfer Next", Size = 10, Quantity = 1, UnitPrice = 259.00m, TotalPrice = 259.00m }, // Product 1, Size 10
                new OrderItem { Id = 2, OrderId = 1, ProductVariantId = 11, ProductName = "Aero Burst", Size = 9, Quantity = 1, UnitPrice = 150.00m, TotalPrice = 150.00m }, // Product 2, Size 9
                new OrderItem { Id = 3, OrderId = 2, ProductVariantId = 12, ProductName = "Aero Burst", Size = 10, Quantity = 1, UnitPrice = 150.00m, TotalPrice = 150.00m }, // Product 2, Size 10
                new OrderItem { Id = 4, OrderId = 3, ProductVariantId = 4, ProductName = "Cloudsurfer Next", Size = 11, Quantity = 1, UnitPrice = 259.00m, TotalPrice = 259.00m } // Product 1, Size 11
            );
            // Seed single admin
            modelBuilder.Entity<Admin>().HasData(
                new Admin { AdminId = 1, UserName = "Admin User", Phone = "+65 1234 5678", DOB = new DateTime(1990, 1, 1), Email = "admin@goodshoe.com", Currency = "SGD", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) }
            );
        }
        
        private int GetStockForProduct(int productId, int size)
        {
            // Distribute stock based on your original data
            return productId switch
            {
                1 => size >= 8 && size <= 12 ? 3 : 0, // Cloudsurfer Next: sizes 8-12
                2 => size >= 8 && size <= 11 ? 7 : 0, // Aero Burst: sizes 8-11
                3 => size >= 10 && size <= 14 ? 5 : 0, // GoodShoe 0.1: sizes 10-14
                4 => size >= 8 && size <= 12 ? 8 : 0, // GoodShoe 0.2: sizes 8-12
                5 => size >= 9 && size <= 13 ? 6 : 0, // GoodShoe 0.3: sizes 9-13
                6 => size >= 10 && size <= 15 ? 15 : 0, // Nike Max 260: sizes 10-15
                _ => 0
            };
        }
    }
}