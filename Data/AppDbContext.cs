using E_Commerce2.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce2.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Product> Products { get; set; }
    }
}
