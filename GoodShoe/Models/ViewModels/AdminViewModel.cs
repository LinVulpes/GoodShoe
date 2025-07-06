using System.ComponentModel.DataAnnotations;
using GoodShoe.Models;

namespace GoodShoe.ViewModels
{
    // Admin Dashboard
    public class AdminDashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int PendingOrders { get; set; }
        public int LowStock { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<RecentOrderViewModel> RecentOrders { get; set; } = new();
    }

    public class RecentOrderViewModel
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int Items { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public string StatusColor { get; set; } = string.Empty;
    }

    // Product Management
    public class ProductManagementViewModel
    {
        public List<ProductListItemViewModel> Products { get; set; } = new();
        public string SearchTerm { get; set; } = string.Empty;
        public string CategoryFilter { get; set; } = string.Empty;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalProducts { get; set; }
    }

    public class ProductListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockCount { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsLowStock { get; set; }
        public bool IsOutOfStock { get; set; }
    }

    public class ProductFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Brand is required")]
        [StringLength(50, ErrorMessage = "Brand cannot exceed 50 characters")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 9999.99, ErrorMessage = "Price must be between $0.01 and $9999.99")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Stock count is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock count must be 0 or greater")]
        public int StockCount { get; set; }

        [Required(ErrorMessage = "Color is required")]
        public string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "At least one size must be selected")]
        public List<string> SelectedSizes { get; set; } = new();

        // For form display
        public List<string> AvailableCategories { get; set; } = new() { "Men", "Women", "Unisex" };
        public List<string> AvailableSizes { get; set; } = ShoeSizes.Available.ToList();

        public bool IsEditMode => Id > 0;
        public string PageTitle => IsEditMode ? "Edit Product" : "Add New Product";
        public string SubmitButtonText => IsEditMode ? "Update Product" : "Create Product";

        // Convert from Product model
        public static ProductFormViewModel FromProduct(Product product)
        {
            return new ProductFormViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Brand = product.Brand,
                Price = product.Price,
                Description = product.Description,
                StockCount = product.StockCount,
                Color = product.Color,
                Category = product.Category,
                ImageUrl = product.ImageUrl,
                SelectedSizes = product.GetSizesList()
            };
        }

        // Convert to Product model
        public Product ToProduct()
        {
            return new Product
            {
                Id = Id,
                Name = Name,
                Brand = Brand,
                Price = Price,
                Description = Description,
                StockCount = StockCount,
                Color = Color,
                Category = Category,
                ImageUrl = ImageUrl,
                AvailableSizes = string.Join(",", SelectedSizes)
            };
        }
    }

    // Admin Settings
    public class AdminSettingsViewModel
    {
        public int AdminID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;

        [StringLength(20)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [StringLength(10)]
        [Display(Name = "Currency")]
        public string Currency { get; set; } = "SGD";

        public List<string> Currencies { get; set; } = new() { "SGD", "USD"};
    }
}