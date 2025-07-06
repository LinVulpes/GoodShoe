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
            var products = _context.Product.AsQueryable();

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
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        
        // Getting Category-specific pages
        public async Task<IActionResult> Men()
        {
            var menProducts = await _context.Product.Where(p => p.Category == "Men").ToListAsync();
            return View("Index", menProducts);
        }
        
        public async Task<IActionResult> Women()
        {
            var womenProducts = await _context.Product.Where(p => p.Category == "Women").ToListAsync();
            return View("Index", womenProducts);
        }
        
        public async Task<IActionResult> Unisex()
        {
            var unisexProducts = await _context.Product.Where(p => p.Category == "Unisex").ToListAsync();
            return View("Index", unisexProducts);
        }
        

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Brand,Price,Size,Description,StockCount,Color,Category")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
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

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Brand,Price,Size,Description,StockCount,Color,Category")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
