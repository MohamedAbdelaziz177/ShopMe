using E_Commerce2.Models;

namespace E_Commerce2.Repositories.IRepositories
{
    public interface ICartRepo : IGenericRepo<Cart>
    {
        Task<List<Cart>> GetCartsByUserId(string userId);
        Task<bool> GetCartByProductAndUser(int productId, string userId);
    }
}
