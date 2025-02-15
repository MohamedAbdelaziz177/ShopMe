﻿using System.ComponentModel.DataAnnotations;

namespace E_Commerce2.ViewModels.OderVM_s
{
    public class OrderVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Delivery Address is required.")]
        [MaxLength(200)]
        public string DeliveryAddress { get; set; } = "";
        public string PaymentMethod { get; set; } = "";

        public decimal? SubTotal { get; set; }
    }
}
