using GoodShoe.Models;
using System.ComponentModel.DataAnnotations;

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
}