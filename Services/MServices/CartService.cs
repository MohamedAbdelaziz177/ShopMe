using E_Commerce2.Models;
using E_Commerce2.Services.IServices;
using E_Commerce2.UnitOfWorkk;
using E_Commerce2.ViewModels.CartVM_s;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce2.Services.MServices
{
    public class CartService : ICartService 
    {
        private readonly IUnitOfWork unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> inCart(int productId, string userID)
        {
            bool isFound = await unitOfWork.CartRepo.GetCartByProductAndUser(productId, userID);
            return isFound;
        }

        public async Task<List<CartVM>> MapCartsToVMs(List<Cart> carts)
        {
            var lst = new List<CartVM>();

            foreach (var cart in carts)
            {
                lst.Add(await MapCartToVM(cart));
            }

            return lst;
        }

        public async Task<CartVM> MapCartToVM(Cart cart)
        {
            var allCarts = await unitOfWork.CartRepo.GetAllAsync();
            var allProducts = await  unitOfWork.ProductRepo.GetAllAsync();


            var result =  (from Carts in allCarts
                                join product in allProducts on cart.ProductId equals product.Id
                                where Carts.Id == cart.Id
                                select new CartVM
                                {
                                    Id = cart.Id,
                                    UserId = cart.CustomerId,
                                    ProductId = cart.ProductId,
                                    Quantity = cart.quantity,
                                    ProductImg = product.ImageFileName,
                                    ProductName = product.Name,
                                    ProductCategory = product.Category,
                                    ProductBrand = product.Brand,
                                    Price = product.Price
                                }).FirstOrDefault();

            return result;

        }

    }
}
