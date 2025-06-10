using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using GoodShoe.Data;
using GoodShoe.Models;
using GoodShoe.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GoodShoe.Controllers;

public class HomeController : Controller
{
    private readonly GoodShoeContext _logger;

    public HomeController(GoodShoeContext logger)
    {
        _logger = logger;
    }
    
    // Adding Filters
    public async Task<IActionResult> Index(string sortOrder, string searchString, string genderFilter)
    {
        // Viewing data from dropdowns
        ViewBag.CurrentSort = sortOrder;
        ViewBag.CurrentFilter = searchString;
        ViewBag.CurrentGender = genderFilter;

        var products = from p in _logger.Product
            select p;

        // Search filter
        if (!String.IsNullOrEmpty(searchString))
        {
            products = products.Where(p => p.Name.Contains(searchString) 
                                           || p.Brand.Contains(searchString)
                                           || p.Description.Contains(searchString));
        }
            
        // Gender Filter
        if (!String.IsNullOrEmpty(genderFilter))
        {
            products = products.Where(p => p.Gender == genderFilter);
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
        var allProducts = _logger.Product;
        var menCount = await allProducts.CountAsync(p => p.Gender == "Men");
        var womenCount = await allProducts.CountAsync(p => p.Gender == "Women");
        var unisexCount = await allProducts.CountAsync(p => p.Gender == "Unisex");

        var viewModel = new ProductListViewModel
        {
            Products = await products.ToListAsync(),
            SortOrder = sortOrder ?? "",
            CurrentFilter = searchString ?? "",
            GenderFilter = genderFilter ?? "",
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}