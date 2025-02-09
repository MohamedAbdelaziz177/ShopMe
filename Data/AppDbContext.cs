using E_Commerce2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce2.Data
{
    public class AppDbContext : IdentityDbContext<AppUser> 
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

       DbSet<Product> Products { get; set; }
       DbSet<Cart> Carts { get; set; }
       DbSet<OrderItem> OrderItems { get; set; }
       DbSet<Order> Orders { get; set; }
    }
}
