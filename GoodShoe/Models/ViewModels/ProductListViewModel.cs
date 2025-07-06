using GoodShoe.Models;

namespace GoodShoe.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public string SortOrder { get; set; } = string.Empty;
        public string CurrentFilter { get; set; } = string.Empty;
        public string CategoryFilter { get; set; } = string.Empty; // Changed from GenderFilter
        public int TotalProducts { get; set; }
        
        // Category navigation (updated from Gender)
        public int MenCount { get; set; }
        public int WomenCount { get; set; }
        public int UnisexCount { get; set; }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<string> AvailableSizes { get; set; } = new();
        public bool IsInStock { get; set; }
        public int StockCount { get; set; }
    }
}