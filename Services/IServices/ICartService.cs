using E_Commerce2.Models;
using E_Commerce2.ViewModels.CartVM_s;

namespace E_Commerce2.Services.IServices
{
    public interface ICartService
    {
        public Task AddToCart(int productId, int userId);

        public Task<List<CartVM>> GetAllCarts(int userId);
        public Task<decimal> getTheTotalCost(int userId);
        public Task EditQuantity(CartVM cartVM);

        public Task<CartVM> GetCartVMbyCartId(int Cartid, int userId);
    }
}
