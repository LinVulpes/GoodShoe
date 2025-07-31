using GoodShoe.Data;
using GoodShoe.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodShoe.Data
{
    public static class DbInitializer // Product Seed
    {
        public static void Initialize(GoodShoeDbContext context)
        {
            // Testing Database with Test Cases
            // Test 1 : Start Database //
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
                    ProductId = 1,
                    Name = "Cloudsurfer Next",
                    Brand = "Puma",
                    Price = 259.00M,
                    Description = "Lace up in Swiss-engineered runners with these Cloudsurfer Next trainers from On Running.",
                    Color = "White",
                    Category = "Unisex",
                    ImageUrl = "/images/products/image1.png",
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Aero Burst",
                    Brand = "Sketchers",
                    Price = 150.00M,
                    Description = "Hit every mile marker in long-distance confidence and premium cushioned comfort with Skechers Aero Burst™. This well-cushioned running style has been granted the APMA Seal of Acceptance and is designed for the daily runner.",
                    Color = "Periwinkle",
                    Category = "Women",
                    ImageUrl = "/images/products/image2.png",
                },

                // GoodShoe Collection
                new Product
                {
                    ProductId = 3,
                    Name = "GoodShoe 0.1",
                    Brand = "GoodShoe",
                    Price = 100.00M,
                    Description = "Men's Shoes",
                    Color = "Brown",
                    Category = "Men",
                    ImageUrl = "/images/products/image3.png",
                },
                new Product
                {
                    ProductId = 4,
                    Name = "GoodShoe 0.2",
                    Brand = "GoodShoe",
                    Price = 100.00M,
                    Description = "Women's Shoes",
                    Color = "Brown",
                    Category = "Men",
                    ImageUrl = "/images/products/image4.png",
                },
                new Product
                {
                    ProductId = 5,
                    Name = "GoodShoe 0.3",
                    Brand = "GoodShoe",
                    Price = 100.00M,
                    Description = "Unisex Shoes",
                    Color = "Black",
                    Category = "Men",
                    ImageUrl = "/images/products/image5.png",
                },
                new Product
                {
                    ProductId = 6,
                    Name = "Nike Max 260",
                    Brand = "Nike",
                    Price = 149.99M,
                    Description = "The Nike Air Max 270 delivers visible cushioning under every step.",
                    Color = "White",
                    Category = "Men",
                    ImageUrl = "/images/products/image6.png",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            context.Product.AddRange(products);
            var savedCount = context.SaveChanges();
            Console.WriteLine($"DbInitializer: Saved {savedCount} products to database");

            // Add ProductVariants for each product (sizes 8-16)
            Console.WriteLine("DbInitializer: Adding product variants (sizes 8-16) ...");

            var productVariants = new List<ProductVariant>();
            var sizes = new int[] { 8, 9, 10, 11, 12, 13, 14, 15, 16 };

            foreach (var product in products)
            {
                foreach (var size in sizes)
                {
                    var stockCount = GetStockForProduct(product.ProductId, size);
                    if (stockCount > 0) // Only add variants with stock
                    {
                        productVariants.Add(new ProductVariant
                        {
                            ProductId = product.ProductId,
                            Size = size,
                            StockCount = stockCount
                        });
                    }
                }
            }

            context.ProductVariant.AddRange(productVariants);
            var variantsSaved = context.SaveChanges();
            Console.WriteLine($"DbInitializer: Saved {variantsSaved} product variants to database");
        }

        private static int GetStockForProduct(int productId, int size)
        {
            // Define which sizes are available for each product and their stock
            return productId switch
            {
                1 => size >= 8 && size <= 12 ? 3 : 0, // Cloudsurfer Next: sizes 8-12, stock 3 each
                2 => size >= 8 && size <= 11 ? 7 : 0, // Aero Burst: sizes 8-11, stock 7 each
                3 => size >= 10 && size <= 14 ? 5 : 0, // GoodShoe 0.1: sizes 10-14, stock 5 each
                4 => size >= 8 && size <= 12 ? 5 : 0, // GoodShoe 0.2: sizes 8-12, stock 5 each
                5 => size >= 9 && size <= 13 ? 10 : 0, // GoodShoe 0.3: sizes 9-13, stock 10 each
                6 => size >= 10 && size <= 15 ? 15 : 0, // Nike Max 260: sizes 10-15, stock 15 each
                _ => 0
            };
        }

        // Initializing IServiceProvider
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<GoodShoeDbContext>())
            {
                Initialize(context);
            }

            // Seed roles & default admin user
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string[] roles = { "Manager", "Member" };

                foreach (var role in roles)
                {
                    if (!roleManager.RoleExistsAsync(role).Result)
                    {
                        var result = roleManager.CreateAsync(new IdentityRole(role)).Result;
                        Console.WriteLine($"Created role: {role} = {result.Succeeded}");
                    }
                }

                // Add default admin user
                var adminEmail = "admin@goodshoe.com";
                var adminUser = userManager.FindByEmailAsync(adminEmail).Result;

                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var result = userManager.CreateAsync(adminUser, "Admin123!").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(adminUser, "Manager").Wait();
                        Console.WriteLine("Default admin user created and added to 'Manager' role.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to create admin user.");
                    }
                }
                else
                {
                    Console.WriteLine("Admin user already exists.");
                }
            }
        }
    }
}
