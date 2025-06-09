namespace GoodShoe.Models.ViewModels;

public class ProductListViewModel
{
    public IEnumerable<Product> Products { get; set; } = new List<Product>();
    public string SortOrder { get; set; }
    public string CurrentFilter {get; set;}
    public int TotalProducts { get; set; }
}