using Microsoft.EntityFrameworkCore;
using GoodShoe.Data;
using GoodShoe.Models;

namespace GoodShoe.Data
{
    public static class DbInitializer
    {
        private static void MigrateImagesToDatabase(GoodShoeDbContext context)
        {
            Console.WriteLine("DbInitializer: Migrating images to database...");
            
            var products = context.Product
                .Where(p => !string.IsNullOrEmpty(p.ImageUrl) && p.Image == null)
                .ToList();
            
            if (!products.Any())
            {
                Console.WriteLine("DbInitializer: No images to migrate.");
                return;
            }
            
            var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    
            foreach (var product in products)
            {
                try
                {
                    // Convert relative URL to file path
                    var imagePath = product.ImageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
                    var fullPath = Path.Combine(wwwrootPath, imagePath);

                    if (File.Exists(fullPath))
                    {
                        var imageData = File.ReadAllBytes(fullPath);
                        product.Image = imageData;
                        product.ImageFileName = Path.GetFileName(fullPath);
                
                        Console.WriteLine($"Migrated image for product {product.ProductId}: {product.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Image file not found for product {product.ProductId}: {fullPath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error migrating image for product {product.ProductId}: {ex.Message}");
                }
            }
    
            context.SaveChanges();
            Console.WriteLine($"DbInitializer: Migrated {products.Count(p => p.Image != null)} images to database.");
        }
        
        public static void Initialize(GoodShoeDbContext context)
        {
            Console.WriteLine("DbInitializer: Starting ....");
            
            // Apply pending migrations
            context.Database.Migrate();
            
            // Only handle image migration now
            var productsNeedingImageMigration = context.Product
                .Any(p => !string.IsNullOrEmpty(p.ImageUrl) && p.Image == null);
        
            if (productsNeedingImageMigration)
            {
                MigrateImagesToDatabase(context);
            }
            else
            {
                Console.WriteLine("DbInitializer: All images already migrated.");
            }
            
            Console.WriteLine("DbInitializer: Completed.");
        }
        
        // Initializing IServiceProvider
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<GoodShoeDbContext>())
            {
                Initialize(context);
            }
        }
    }
}