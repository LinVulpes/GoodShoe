using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodShoe.Data;
using GoodShoe.Models;

namespace GoodShoe.Controllers
{
    public class ProductsController : Controller
    {
        private readonly GoodShoeDbContext _context;

        public ProductsController(GoodShoeDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string Category = "")
        {
            var products = _context.Product.Include(p => p.ProductVariants).AsQueryable();

            if (!string.IsNullOrEmpty(Category))
            {
                products = products.Where(p => p.Category == Category);
            }
            
            return View(await products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductVariants)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            // Debug: Check if variants are loaded
            Console.WriteLine($"Product {product.Name} has {product.ProductVariants?.Count ?? 0} variants");

            return View(product);
        }
        
        // Method to check size availability
        [HttpGet]
        public async Task<IActionResult> CheckSizeAvailability(int productId, int size)
        {
            var productVariant = await _context.ProductVariant
                .FirstOrDefaultAsync(pv => pv.ProductId == productId && pv.Size == size);
            
            if (productVariant == null)
            {
                return Json(new { available = false, stock = 0 });
            }

            return Json(new
            {
                available = productVariant.StockCount > 0,
                stock = productVariant.StockCount
            });
        }
        
        // Get all available sizes for a product
        [HttpGet]
        public async Task<IActionResult> GetAvailableSizes(int productId)
        {
            var productVariants = await _context.ProductVariant
                .Where(pv => pv.ProductId == productId)
                .OrderBy(pv => pv.Size)
                .ToListAsync();

            if (!productVariants.Any())
            {
                return Json(new List<object>());
            }

            var availableSizes = productVariants
                .Select(pv => new
                {
                    size = $"US {pv.Size}",
                    sizeValue = pv.Size,
                    stock = pv.StockCount,
                    available = pv.StockCount > 0
                })
                .ToList();
            
            return Json(availableSizes);
        }
        
        // Getting Category-specific pages
        public async Task<IActionResult> Men()
        {
            var menProducts = await _context.Product
                .Include(p => p.ProductVariants)
                .Where(p => p.Category == "Men")
                .ToListAsync();
            return View("Index", menProducts);
        }
        
        public async Task<IActionResult> Women()
        {
            var womenProducts = await _context.Product
                .Include(p => p.ProductVariants)
                .Where(p => p.Category == "Women")
                .ToListAsync();
            return View("Index", womenProducts);
        }
        
        public async Task<IActionResult> Unisex()
        {
            var unisexProducts = await _context.Product
                .Include(p => p.ProductVariants)
                .Where(p => p.Category == "Unisex")
                .ToListAsync();
            return View("Index", unisexProducts);
        }
        
        // Method to put images from database
        public async Task<IActionResult> GetImage(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product?.Image == null)
            {
                return NotFound();
            }
            return File(product.Image, "image/jpeg");
        }
        
        // Helper Method to convert File to byte array
        private async Task<byte[]> ConvertToByteArrayAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Brand,Price,Description,Color,Category,ImageUrl")] Product product, IFormFile imageFile)
        {   
            // Handle image upload if provided
            if (imageFile != null && imageFile.Length > 0)
            {
                var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png"};
                if (allowedTypes.Contains(imageFile.ContentType.ToLower()))
                {
                    product.Image = await ConvertToByteArrayAsync(imageFile);
                    product.ImageFileName = imageFile.FileName;
                }
            }
            
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                
                // Create ProductVariants for sizes 8-16 with 0 stock initially
                var sizes = new int[] { 8, 9, 10, 11, 12, 13, 14, 15, 16 };
                foreach (var size in sizes)
                {
                    var variant = new ProductVariant
                    {
                        ProductId = product.ProductId,
                        Size = size,
                        StockCount = 0
                    };
                    _context.ProductVariant.Add(variant);
                }
   
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductVariants)
                .FirstOrDefaultAsync(p => p.ProductId == id);
            
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Brand,Price,Description,Color,Category,ImageUrl,CreatedAt")] Product product, IFormFile imageFile)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingProduct = await _context.Product.FindAsync(id);
                    if (existingProduct == null)
                    {
                        return NotFound();
                    }
                    
                    // Update basic properties
                    existingProduct.Name = product.Name;
                    existingProduct.Brand = product.Brand;
                    existingProduct.Price = product.Price;
                    existingProduct.Description = product.Description;
                    existingProduct.Color = product.Color;
                    existingProduct.Category = product.Category;
                    existingProduct.ImageUrl = product.ImageUrl;

                    // Handle image upload if provided
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png"};
                        if (allowedTypes.Contains(imageFile.ContentType.ToLower()))
                        {
                            existingProduct.Image = await ConvertToByteArrayAsync(imageFile);
                            existingProduct.ImageFileName = imageFile.FileName;
                        }
                    }
                    
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/EditStock/5 - New method for managing stock
        public async Task<IActionResult> EditStock(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductVariants)
                .FirstOrDefaultAsync(p => p.ProductId == id);
            
            if (product == null)
            {
                return NotFound();
            }
            
            return View(product);
        }

        // POST: Products/EditStock/5 - Update stock for variants
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStock(int id, Dictionary<int, int> stockCounts)
        {
            var product = await _context.Product
                .Include(p => p.ProductVariants)
                .FirstOrDefaultAsync(p => p.ProductId == id);
                
            if (product == null)
            {
                return NotFound();
            }

            try
            {
                foreach (var variant in product.ProductVariants)
                {
                    if (stockCounts.ContainsKey(variant.Size))
                    {
                        variant.StockCount = stockCounts[variant.Size];
                    }
                }
                
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductVariants)
                .FirstOrDefaultAsync(p => p.ProductId == id);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product
                .Include(p => p.ProductVariants)
                .FirstOrDefaultAsync(p => p.ProductId == id);
                
            if (product != null)
            {
                // ProductVariants will be deleted automatically due to cascade delete
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId== id);
        }

        // Helper method to get total stock for a product
        [HttpGet]
        public async Task<IActionResult> GetProductStock(int productId)
        {
            var totalStock = await _context.ProductVariant
                .Where(pv => pv.ProductId == productId)
                .SumAsync(pv => pv.StockCount);

            var availableSizes = await _context.ProductVariant
                .Where(pv => pv.ProductId == productId && pv.StockCount > 0)
                .CountAsync();

            return Json(new 
            { 
                totalStock = totalStock,
                availableSizes = availableSizes
            });
        }
    }
}