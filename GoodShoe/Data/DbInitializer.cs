using Microsoft.EntityFrameworkCore;
using GoodShoe.Data;
using GoodShoe.Models;

namespace GoodShoe.Data
{
    public static class DbInitializer // Product Seed
    {
        public static void Initialize(GoodShoeContext context)
        {
            // Testing Database with Test Cases
            // Test 1 : Start Databse //
            Console.WriteLine("DbInitializer: Starting ....");
            
            // Ensure the database is created
            context.Database.EnsureCreated();
            
            // Look for any products
            if (context.Product.Any())
            { 
                // Test 2 : Test finding of products //
                Console.WriteLine("DbInitializer: Products already exist.");
                return;
            }
            
            // Test 3 : Database adding seed data  //
            Console.WriteLine("DbInitializer: Adding seed data/products ...");
            // Add existing seed data
            var products = new Product[]
            {
                new Product
                {
                    Id = 1,
                    Name = "Cloudsurfer Next",
                    Brand = "Puma",
                    Price = 259.00M,
                    Size = 7,
                    Description = "Lace up in Swiss-engineered runners with these Cloudsurfer Next trainers from On Running.",
                    StockCount = 3,
                    Color = "White",
                    Gender = "Unisex",
                    ImageUrl = "images/products/image1.png",
                    IsActive = true
                },
                new Product
                {
                    Id = 2,
                    Name = "Aero Burst",
                    Brand = "Sketchers",
                    Price = 150.00M,
                    Size = 6.5M,
                    Description = "Hit every mile marker in long-distance confidence and premium cushioned comfort with Skechers Aero Burst™. This well-cushioned running style has been granted the APMA Seal of Acceptance and is designed for the daily runner.",
                    StockCount = 7,
                    Color = "Periwinkle",
                    Gender = "Women",
                    ImageUrl = "images/products/image2.png",
                    IsActive = true
                },
                
                // GoodShoe Collection
                new Product
                {
                    Id = 3,
                    Name = "GoodShoe 0.1",
                    Brand = "GoodShoe",
                    Price = 100.00M,
                    Size = 8,
                    Description = "Men's Shoes",
                    StockCount = 5,
                    Color = "Brown",
                    Gender = "Men",
                    ImageUrl = "images/products/image3.png",
                    IsActive = true
                },
            };
            context.Product.AddRange(products);
            var savedCount = context.SaveChanges();
            Console.WriteLine($"DbInitializer: Saved {savedCount} products to database");
        }
        
        // Initializing IServiceProvider
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<GoodShoeContext>())
            {
                Initialize(context);
            }
        }
    }
}