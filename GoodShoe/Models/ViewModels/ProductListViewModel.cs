namespace GoodShoe.Models.ViewModels;

public class ProductListViewModel
{
    public IEnumerable<Product> Products { get; set; } = new List<Product>();
    public string SortOrder { get; set; } = string.Empty;
    public string CurrentFilter {get; set;} = string.Empty;
    public string GenderFilter { get; set; } = string.Empty;
    public int TotalProducts { get; set; }
    
    // Gender navigation
    public int MenCount { get; set; }
    public int WomenCount { get; set; }
    public int UnisexCount { get; set; }
}