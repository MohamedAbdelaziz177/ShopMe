using Microsoft.AspNetCore.Mvc;

namespace E_Commerce2.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
