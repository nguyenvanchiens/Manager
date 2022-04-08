namespace Manager.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string? Discount { get; set; }
        public string? LinkImage { get; set; }
    }
}
