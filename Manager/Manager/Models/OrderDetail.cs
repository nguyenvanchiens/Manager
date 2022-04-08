namespace Manager.Models
{
    public class OrderDetail
    {
        [Key]
        public Guid OrderDetailId { get; set; }
        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? Country { get; set; } = string.Empty;
        public string? LinkImage { get; set; }
        public string? PromotionalCode { get; set; }
        public string? Image { get; set; }
    }
}
