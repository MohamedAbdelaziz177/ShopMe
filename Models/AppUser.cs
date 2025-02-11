﻿using Microsoft.AspNetCore.Identity;

namespace E_Commerce2.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string? Address { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}
