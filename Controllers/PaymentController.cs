using E_Commerce2.UnitOfWorkk;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe.V2;

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
            domain = "http://localhost:5001";
        }


        
     //public async Task<IActionResult> CreatePaymentSession(int id)
     //{
     //    var order = await unitOfWork.OrderRepo.GetByIdAsync(id);
     //
     //    var options = new SessionCreateOptions
     //    {
     //        PaymentMethodTypes = new List<string> { "Card" },
     //        Mode = "Payment",
     //        SuccessUrl = $"{domain}/Payment/Success",
     //        CancelUrl = $"{domain}/Payment/Cancel",
     //
     //        LineItems = new List<SessionLineItemOptions>()
     //
     //    };
     //
     //    foreach (var orderItem in order.Items) 
     //    {
     //
     //    }
     //}
         

    }
}
