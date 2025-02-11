using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce2.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string CustomerId { get; set; } = string.Empty;
     
        public int ProductId { get; set; }

        public int quantity { get; set; }

        public DateTime UpdatedAt { get; set; }

       

        // public decimal Total { get; set; }

        public Product Product { get; set; } = new Product();
        public AppUser Customer { get; set; } = new AppUser();
    }
}
