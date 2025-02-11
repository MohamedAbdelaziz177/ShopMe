using E_Commerce2.Data;
using E_Commerce2.Models;
using E_Commerce2.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce2.Repositories.MRepositories
{
    public class CartRepo : GenericRepo<Cart>, ICartRepo
    {
        public CartRepo(AppDbContext con) : base(con)
        {
        }

        public async Task<bool> GetCartByProductAndUser(int productId, string userId)
        {
            
            return dbset.Any(x => x.ProductId == productId && x.CustomerId == userId);
        }

        public async Task<List<Cart>> GetCartsByUserId(string userId)
        {
            return await dbset.Where( x => x.CustomerId == userId).ToListAsync();
        }
    }
}
