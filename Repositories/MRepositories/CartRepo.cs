using E_Commerce2.Data;
using E_Commerce2.Models;
using E_Commerce2.Repositories.IRepositories;

namespace E_Commerce2.Repositories.MRepositories
{
    public class CartRepo : GenericRepo<Cart>, ICartRepo
    {
        public CartRepo(AppDbContext con) : base(con)
        {
        }
    }
}
