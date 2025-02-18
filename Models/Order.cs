using Microsoft.EntityFrameworkCore;

namespace E_Commerce2.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string CustomerId { get; set; } 
        public AppUser Customer { get; set; } 

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        [Precision(16, 2)]
        public decimal ShippingFee { get; set; } = 12.5M;

        public decimal SubTotal { get; set; } = 0;

        public string DeliveryAddress { get; set; } = "";
        public string PaymentMethod { get; set; } = "";
        public string PaymentStatus { get; set; } = "";
        public string PaymentDetails { get; set; } = ""; 
        public string OrderStatus { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
