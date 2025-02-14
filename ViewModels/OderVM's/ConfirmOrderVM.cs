using E_Commerce2.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce2.ViewModels.OderVM_s
{
    public class ConfirmOrderVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Delivery Address is required.")]
        [MaxLength(200)]
        public string DeliveryAddress { get; set; } = "";
        public string PaymentMethod { get; set; } = "";

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
