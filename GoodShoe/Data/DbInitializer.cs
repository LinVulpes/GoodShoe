using Microsoft.EntityFrameworkCore;
using GoodShoe.Data;
using GoodShoe.Models;

namespace GoodShoe.Data
{
    public static class DbInitializer
    {
        public static void Initialize(GoodShoeContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();
            
            // Look for any products
            if (context.Product.Any())
            { 
                return; // DB has been seeded.
            }
            
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
                    Gender = "Unisex"
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
                    Gender = "Women"
                }
            };
            context.Product.AddRange(products);
            context.SaveChanges();
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