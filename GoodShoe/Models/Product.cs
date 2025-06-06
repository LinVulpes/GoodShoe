namespace GoodShoe.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public decimal Price { get; set; }
        public decimal Size {  get; set; }
        public string? Description { get; set; }
        public int StockCount { get; set; }

        public string? Color { get; set; }
        public string? Gender { get; set; }
    }
}
