namespace Manager.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string? OrderCode { get; set; }
        public OrderStatus Status { get; set; }
        public string? Payment { get; set; }
        public string? Delivery { get; set; }
        public string? UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
