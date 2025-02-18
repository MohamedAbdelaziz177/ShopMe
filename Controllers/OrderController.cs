using E_Commerce2.Models;
using E_Commerce2.Services.IServices;
using E_Commerce2.UnitOfWorkk;
using E_Commerce2.ViewModels.CartVM_s;
using E_Commerce2.ViewModels.OderVM_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace E_Commerce2.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOrderService orderService;

        public OrderController(IUnitOfWork unitOfWork, IOrderService orderService)
        {
            this.unitOfWork = unitOfWork;
            this.orderService = orderService;
        }

        [HttpPost]
        public IActionResult CheckOut(List<CartVM> cartItems)
        {
            TempData["CartData"] = JsonConvert.SerializeObject(cartItems);
            TempData.Keep();

            if(!cartItems.Any())
                Console.WriteLine("HAHAHA");

            decimal SubTotal = cartItems.Sum(x => x.Quantity * x.Price);

            TempData["SubTotal"] = JsonConvert.SerializeObject(SubTotal); 
            TempData.Keep();

            Console.WriteLine(SubTotal);

            ViewBag.SubTotal = SubTotal;


            return View("CheckOut");

        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderVM order)
        {

            TempData.Keep("CartData");
            TempData.Keep("SubTotal");

            

            string userId = User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";
           

            Order ord = await orderService.CreateOrder(order, userId, (decimal)order.SubTotal);

            var cartDataJson = TempData["CartData"] as string;

            if (cartDataJson == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var cartItems = JsonConvert.DeserializeObject<List<CartVM>>(cartDataJson);

            var orderItems = await orderService.InsertOrderItems(cartItems);

            ord.Items = orderItems;

            return RedirectToAction("ConfirmOrder", new { id = ord.Id });

        }

       
        public async Task<IActionResult> ConfirmOrder(int id)
        {
             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
             Order ord = await unitOfWork.OrderRepo.GetByIdAsync(id);

             OrderVM ordVM = new OrderVM();
             ordVM.Id = id;
             ordVM.PaymentMethod = ord.PaymentMethod;
             ordVM.DeliveryAddress = ord.DeliveryAddress;
             
           

            return View(ordVM);
        }

        public IActionResult GetOrderByID(int id)
        {
            var Order = unitOfWork.OrderRepo.GetByIdAsync(id);
            return View(Order);
        }

        public async Task<IActionResult> CancelOrder(int id)
        {
            var Order = await unitOfWork.OrderRepo.GetByIdAsync(id);
            Order.OrderStatus = "Canceled";

            return View();
        }


    }
}
