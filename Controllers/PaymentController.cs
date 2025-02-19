using E_Commerce2.UnitOfWorkk;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Stripe.V2;
using System.Security.Cryptography.Xml;

namespace E_Commerce2.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitOfWork;
        private readonly string domain;

        public PaymentController(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
           // domain = $"{Request.Scheme}://{Request.Host}";
        }


        //[HttpPost("CreatePaymentSession")]
        public async Task<IActionResult> CreatePaymentSession(int id)
        {
            var order = await unitOfWork.OrderRepo.GetOrderItemsByOrderId(id);

            if (order == null)
            {
                Console.WriteLine("Product is null");
            }

            /*
             
             var payIntentOptions = new PaymentIntentCreateOptions()
            {
                Currency = "usd",
                Amount = (long)order.SubTotal * 100,
                PaymentMethodTypes = new List<string>() { "card" }


            };


            var payIntentService = new PaymentIntentService();
            var paymentIntent = await payIntentService.CreateAsync(payIntentOptions);

             */


            var lineItems = new List<SessionLineItemOptions>();
        
            foreach (var item in order.Items)
            {
                if (item == null)
                {
                    Console.WriteLine("item is null");
                }

                if (item.Product == null)
                {
                    Console.WriteLine("item.Product is null");
                }

                lineItems.Add(new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        Currency = "usd",
                        UnitAmountDecimal = item.UnitPrice * 100,

                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = !string.IsNullOrEmpty(item.Product?.Name) ? item.Product.Name : "Unnamed Product"
                        },

                        
                    },

                    Quantity = item.Quantity,
                    
                });
            
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/Payment/Success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/Payment/Cancel",
                

                LineItems = lineItems,

            };
        
        
            var service = new SessionService();
            var Session = service.Create(options);

            Console.WriteLine($"Session ID: {Session.Id}");
            Console.WriteLine($"PaymentIntentId: {Session.PaymentIntentId}");

            order.PaymentIntentId = Session.PaymentIntentId;
            await unitOfWork.OrderRepo.updateAsync(order);
           
        
            return Redirect(Session.Url);
            
        }

        public IActionResult Success()
        {

            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }


    }
}
