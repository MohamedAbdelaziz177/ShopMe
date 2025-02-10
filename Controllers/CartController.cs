using E_Commerce2.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce2.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }



    }
}
