using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using GoodShoe.Data;
using GoodShoe.Models;
using GoodShoe.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GoodShoe.Controllers;

public class HomeController : Controller
{
    private readonly GoodShoeDbContext _context;

    public HomeController(GoodShoeDbContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    // Adding Filters
    public async Task<IActionResult> Products(string sortOrder, string searchString, string categoryFilter)
    {
        // Viewing data from dropdowns
        ViewBag.CurrentSort = sortOrder;
        ViewBag.CurrentFilter = searchString;
        ViewBag.CurrentCategory = categoryFilter;
        
        if (_context.Product == null)
        {
            return Problem("No product found!");
        }

        var products = from p in _context.Product.Include(p => p.ProductVariants)
            select p;

        // Apply search filter
        if (!string.IsNullOrEmpty(searchString))
        {
            products = products.Where(p => (p.Name != null && p.Name.ToUpper().Contains(searchString.ToUpper())) 
                                           || (p.Brand != null && p.Brand.ToUpper().Contains(searchString.ToUpper()))
                                           || (p.Description != null && p.Description.ToUpper().Contains(searchString.ToUpper())));
        }
        
        // Gender Filter
        if (!string.IsNullOrEmpty(categoryFilter) && categoryFilter != "All")
        {
            products = products.Where(p => p.Category != null && p.Category == categoryFilter);
        }
        
        // Sorting Filter
        switch (sortOrder)
        {
            case "name_asc":
                products = products.OrderBy(p => p.Name);
                break;
            
            case "name_desc":
                products = products.OrderByDescending(p => p.Name);
                break;
            
            case "price_asc":
                products = products.OrderBy(p => p.Price);
                break;
            
            case "price_desc":
                products = products.OrderByDescending(p => p.Price);
                break;
                
            default:
                products = products.OrderBy(p => p.Name);
                break;
        }
        
        // Gender counts
        var allProducts = _context.Product;
        var menCount = await allProducts.CountAsync(p => p.Category == "Men");
        var womenCount = await allProducts.CountAsync(p => p.Category == "Women");
        var unisexCount = await allProducts.CountAsync(p => p.Category == "Unisex");

        var viewModel = new ProductListViewModel
        {
            Products = await products.ToListAsync(),
            SortOrder = sortOrder ?? "",
            CurrentFilter = searchString ?? "",
            CategoryFilter = categoryFilter ?? "",
            TotalProducts = await products.CountAsync(),
            MenCount = menCount,
            WomenCount = womenCount,
            UnisexCount = unisexCount
        };
        
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult Contact()
    {
        return View();
    }
    
    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}