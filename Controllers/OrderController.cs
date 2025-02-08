using Microsoft.AspNetCore.Mvc;

namespace E_Commerce2.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
