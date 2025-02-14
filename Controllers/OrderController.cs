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

        [HttpPost]
        public IActionResult CheckOut(List<CartVM> cartItems)
        {
            TempData["CartData"] = JsonConvert.SerializeObject(cartItems);
            TempData.Keep();

            if(!cartItems.Any())
                Console.WriteLine("HAHAHA");

            decimal SubTotal = cartItems.Sum(x => x.Quantity * x.Price);

            Console.WriteLine(SubTotal);

            ViewBag.SubTotal = SubTotal;


            return View("CheckOut");

           
        }

        public IActionResult CreateOrder()
        {
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
