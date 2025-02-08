﻿using System.ComponentModel.DataAnnotations;

namespace E_Commerce2.ViewModels.UserVM_s
{
    public class UserLoginVM
    {
        [Required]
        public string Email { get; set; } 

        [Required]
        public string Password { get; set; } 

        public bool RememberMe { get; set; }
    }
}
