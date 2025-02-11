using E_Commerce2.Models;
using E_Commerce2.ViewModels.CartVM_s;

namespace E_Commerce2.Services.IServices
{
    public interface ICartService
    {


         Task<List<CartVM>> MapCartsToVMs(List<Cart> carts);

         Task<CartVM> MapCartToVM(Cart cart);

        Task<bool> inCart(int productId, string UserId);
    }
}
