/*using GoodShoe.Data;
using GoodShoe.Data;
using GoodShoe.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For Products for Customer
namespace GoodShoe.Controllers;

public class ShopController : Controller
{
    private readonly GoodShoeDbContext _context;

    public ShopController(GoodShoeDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Product.ToListAsync();
        return View(products);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        
        var product = await _context.Product
            .FirstOrDefaultAsync(m => m.ProductId == id);
        
        if (product == null) return NotFound();
        
        return View("~/Views/Products/Details.cshtml", product);

    }
}*/