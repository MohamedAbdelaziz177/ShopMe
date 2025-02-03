using E_Commerce2.UnitOfWorkk;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce2.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var lst = await unitOfWork.ProductRepo.GetAllAsync();
            return View(lst);
        }
    }
}
