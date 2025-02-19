using E_Commerce2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace E_Commerce2.Data
{
    public class AppDbContext : IdentityDbContext<AppUser> 
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

     
       

        public DbSet<Product> Products { get; set; }
       public DbSet<Cart> Carts { get; set; }
       public DbSet<OrderItem> OrderItems { get; set; }
       public DbSet<Order> Orders { get; set; }
    }
}
