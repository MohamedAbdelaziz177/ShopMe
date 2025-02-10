using E_Commerce2.Services.IServices;
using E_Commerce2.UnitOfWorkk;
using E_Commerce2.ViewModels.CartVM_s;

namespace E_Commerce2.Services.MServices
{
    public class CartService : ICartService 
    {
        private readonly IUnitOfWork unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task AddToCart(int productId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task EditQuantity(CartVM cartVM)
        {
            throw new NotImplementedException();
        }

        public Task<List<CartVM>> GetAllCarts(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<CartVM> GetCartVMbyCartId(int Cartid, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> getTheTotalCost(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
