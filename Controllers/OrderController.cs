using E_Commerce2.UnitOfWorkk;
using E_Commerce2.ViewModels.CartVM_s;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_Commerce2.Controllers
{
    public class OrderController : Controller
    {
        public OrderController(IUnitOfWork unitOfWork)
        {
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateOrder(List<CartVM> cartItems)
        {
            TempData["CartData"] = JsonConvert.SerializeObject(cartItems);
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmOrder()
        {
            return RedirectToAction("Index");
        }

        public IActionResult GetOrderByID()
        {
            return View();
        }

        public IActionResult CancelOrder()
        {
            return View();
        }


    }
}
