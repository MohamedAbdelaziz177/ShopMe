using System.ComponentModel.DataAnnotations;

namespace E_Commerce2.ViewModels.CartVM_s
{
    public class CartVM
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public string ProductCategory { get; set; } = string.Empty;

        public string ProductBrand { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string ProductImg { get; set; } = string.Empty;
    }
}
