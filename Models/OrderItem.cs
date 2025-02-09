using Microsoft.EntityFrameworkCore;

namespace E_Commerce2.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [Precision(16, 2)]
        public decimal UnitPrice { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = new Product();
    }
}
