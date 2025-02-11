using E_Commerce2.Models;
using E_Commerce2.Services.IServices;
using E_Commerce2.UnitOfWorkk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce2.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICartService cartService;
        private readonly UserManager<AppUser> userManager;

        public CartController(IUnitOfWork unitOfWork, ICartService cartService, UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.cartService = cartService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> GetCartById(int id)
        {
            var cart = await unitOfWork.CartRepo.GetByIdAsync(id);
            var cartVM = await cartService.MapCartToVM(cart);

            return View(cartVM);
                         
        }

        public async Task<ActionResult> AddToCart(int ProductId) 
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (UserId == null) ;

            var cart = new Cart();
            cart.ProductId = ProductId;
            cart.CustomerId = UserId;
            cart.quantity = 1;
            cart.UpdatedAt = DateTime.Now;
           
           cart.Customer = await userManager.FindByIdAsync(UserId);
           cart.Product = await unitOfWork.ProductRepo.GetByIdAsync(ProductId);

            await unitOfWork.CartRepo.insertAsync(cart);

            TempData["ProductId"] = "Added";
            
            // Returning to the products view
            return RedirectToAction("GetCartsforUser");
        }


        public async Task<IActionResult> RemoveFromCart(int CartId)
        {
            bool f = await unitOfWork.CartRepo.deleteAsync(CartId);
            ViewBag.ProductId = "Add To Cart";

            return RedirectToAction("GetCartsforUser");
        }


        public async Task<IActionResult> GetCartsforUser()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (UserId == null);

            var lstCarts = await unitOfWork.CartRepo.GetCartsByUserId(UserId);

            var CartsVM = await cartService.MapCartsToVMs(lstCarts);

            return View(CartsVM);
        }

        public async Task<int> GetCartSize()
        {
            string UserId = User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            if (UserId == null);

            var lstCarts = await unitOfWork.CartRepo.GetCartsByUserId(UserId);

            return lstCarts.Count;
        }

        public async Task<IActionResult> IncCartQuantity(int CartID)
        { 
            var cart = await unitOfWork.CartRepo.GetByIdAsync(CartID);
            cart.quantity += 1;
            await unitOfWork.Complete();

            return RedirectToAction("GetCartsforUser");

        }

        public async Task<IActionResult> DecCartQuantity(int CartID)
        {
            if(CartID == 0)
            {
                return RedirectToAction("GetCartsforUser");
            }
            var cart = await unitOfWork.CartRepo.GetByIdAsync(CartID);
            cart.quantity -= 1;
            await unitOfWork.Complete();

            return RedirectToAction("GetCartsforUser");
        }


        public async Task<bool> IsProductInCart(int productID)
        {
            string userId = User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            bool check = await cartService.inCart(productID, userId);
            
            if(check) ViewBag.IsProductInCart = true;
            else ViewBag.IsProductInCart = false;

            return check;
        }



    }
}
