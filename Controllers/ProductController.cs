using E_Commerce2.Models;
using E_Commerce2.Repositories.MRepositories;
using E_Commerce2.Services.IServices;
using E_Commerce2.UnitOfWorkk;
using E_Commerce2.ViewModels.ProductVM_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_Commerce2.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductService productService;

        public ProductController(IUnitOfWork unitOfWork, IProductService productService)
        {
            this.unitOfWork = unitOfWork;
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var lst = await unitOfWork.ProductRepo.GetAllAsync();
            return View(lst);
        }

       
        [HttpGet]
        public async Task<IActionResult> AddNewProduct()
        {
            var prodVM = await productService.IntializeProductVM();
            return View(prodVM);
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewProduct(ProductCreateUpdateVM prod)
        {

            if(prod.ImageFile == null)
               ModelState.AddModelError("ImageFile", "This Field is Required");
            
            if(!ModelState.IsValid)
                return View("AddNewProduct", prod);


            await productService.SaveProduct(prod);
           
            return RedirectToAction("Index");

        }

        
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await unitOfWork.ProductRepo.GetByIdAsync(id);

            var prodVM = await productService.MapProductModelToVM(product);

            return View(prodVM);
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            await unitOfWork.ProductRepo.deleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
