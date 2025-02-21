using E_Commerce2.Models;
using E_Commerce2.Services.IServices;
using E_Commerce2.UnitOfWorkk;
using E_Commerce2.ViewModels.CartVM_s;
using E_Commerce2.ViewModels.OderVM_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;
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

            using var trx = await unitOfWork.BeginTransaction();
            {

                try
                {
                    Order ord = await orderService.CreateOrder(order, userId, (decimal)order.SubTotal);

                    var cartDataJson = TempData["CartData"] as string;

                    if (cartDataJson == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    var cartItems = JsonConvert.DeserializeObject<List<CartVM>>(cartDataJson);

                    var orderItems = await orderService.InsertOrderItems(cartItems, ord.Id);

                    ord.Items = orderItems;
                    await unitOfWork.Complete();


                    await trx.CommitAsync();

                    return RedirectToAction("CreatePaymentSession", "Payment", new { id = ord.Id });
                }
                catch
                {
                    await trx.RollbackAsync();
                    return RedirectToAction("Index", "Home");
                }


            };

        }

        
        //public async Task<IActionResult> ConfirmOrder(int id)
        //{
        //
        //   var Order = await unitOfWork.OrderRepo.GetByIdAsync(id);
        //   Order.OrderStatus = "Confirmed";
        //   await unitOfWork.Complete();
        //
        //   return View();
        //}
         


        public async Task<IActionResult> CancelOrder(int id)
        {
            var Order = await unitOfWork.OrderRepo.GetByIdAsync(id);

            if (Order == null) return NotFound();

            await RefundOrder(Order.PaymentIntentId);

            Console.WriteLine(Order.PaymentIntentId);

            Order.OrderStatus = "Canceled";

            await unitOfWork.Complete();

            return View();
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShipOrder(int id)
        {
            var Order = await unitOfWork.OrderRepo.GetByIdAsync(id);

            if (Order == null) return NotFound();

            Order.OrderStatus = "Shipped";

            await unitOfWork.Complete();

            return View("GetShippedOrders");

        }

        public async Task<IActionResult> GetOrdersByUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return NotFound();

            var orders = await unitOfWork.OrderRepo.GetOrdersByUserID(userId);

            return View(orders);
            
        }

        public async Task<IActionResult> GetOrderByID(int id)
        {

            //GetOrderItemsByOrderId(int id)
            var Order = await unitOfWork.OrderRepo.GetOrderItemsByOrderId(id);
            return View(Order);
        }

       
        public async Task RefundOrder(string intentId)
        {
            var RefundOptions = new RefundCreateOptions()
            {
                PaymentIntent = intentId,
            };

            var RefundService = new RefundService();
            await RefundService.CreateAsync(RefundOptions);
        }

    }
}
