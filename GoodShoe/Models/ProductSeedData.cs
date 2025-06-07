using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GoodShoe.Data;

namespace GoodShoe.Models
{
    public static class ProductSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GoodShoeContext(
                serviceProvider.GetRequiredService<DbContextOptions<GoodShoeContext>>()))
            {
                //Look for any products
                if (context.Product.Any())
                    {
                        return;
                    }
                context.Product.AddRange(
                    new Product
                    {
                        Id = 1,
                        Name = "Cloudsurfer Next",
                        Brand = "Puma",
                        Price = 259.00M,
                        Size = 7,
                        Description = "Lace up in Swiss-engineered runners with these Cloudsurfer Next trainers from On Running. In a White and Silver colourway, these JD-exclusives have a lightweight, breathable mesh upper with synthetic overlays to keep 'em cool. They feature a lace fastening and a padded collar for a secure fit, while the Helion superfoam midsole and CloudTec Phase cushioning offers a responsive ride as you rack up the miles. Sat on a grippy rubber outsole for total traction, they're signed off with signature On Running branding throughout.",
                        StockCount = 3,
                        Color = "White",
                        Gender = "Unisex"
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Aero Burst",
                        Brand = "Skechers",
                        Price = 150.00M,
                        Size = 6.5M,
                        Description = "Hit every mile marker in long-distance confidence and premium cushioned comfort with Skechers Aero Burst™. This well-cushioned running style has been granted the APMA Seal of Acceptance and is designed for the daily runner.",
                        StockCount = 7,
                        Color = "Periwinkle",
                        Gender = "Women"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}