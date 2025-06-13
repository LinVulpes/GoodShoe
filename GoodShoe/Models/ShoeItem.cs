using System.ComponentModel.DataAnnotations;

namespace GoodShoe.Models
{
    public class ShoeItem
    {
        public int ShoeItemId { get; set; }

        [Required(ErrorMessage = "Please enter a model.")]
        public string ShoeModelId { get; set; }
        public Product Product { get; set; }

        public int size { get; set; }

        public int stock_count { get; set; }
    }
}
