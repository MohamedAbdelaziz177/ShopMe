using E_Commerce2.Models;
using E_Commerce2.Repositories.MRepositories;
using E_Commerce2.Services.IServices;
using E_Commerce2.UnitOfWorkk;
using E_Commerce2.ViewModels.ProductVM_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace E_Commerce2.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductService productService;
        private readonly int PageSize;

        public ProductController(IUnitOfWork unitOfWork, IProductService productService)
        {
            this.unitOfWork = unitOfWork;
            this.productService = productService;
        }


        public async Task<IActionResult> Index(string search, int pageIndex = 0)
        {
            var lst = await unitOfWork.ProductRepo.GetAllAsync();
            lst = lst.OrderByDescending(x => x.Id).ToList();

            int productCount = lst.Count();

            if (search != null)
                lst = lst.Where(x => x.Name.Contains(search) || x.Brand.Contains(search)).ToList();

            if (pageIndex < 1)
            {
                ViewData["search"] = search;
                return View(lst);
            }

            else
            {
                lst = lst.Skip(PageSize * (pageIndex - 1)).Take(PageSize).ToList();
                return View(lst);
            }
        }

        public async Task <IActionResult> GetNewestProducts(string? search, string? cat, string? brand, string? sorting ,int pageIndex = 1)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var lst = await unitOfWork.ProductRepo.GetAllAsync();


            if(search != null)
                lst = lst.Where(x => x.Name.Contains(search)).ToList();

            if(cat != null && cat != "All Categories")
                lst = lst.Where(x => x.Category.Contains(cat)).ToList();

            if(brand != null && brand != "All brands")
                lst = lst.Where(x => x.Brand.Contains(brand)).ToList();

            if (sorting == "newest")
                lst = lst.OrderByDescending(x => x.Id).ToList();
            else if (sorting == "price_asc")
                lst = lst.OrderBy(x => x.Price).ToList();

            else lst = lst.OrderByDescending(x => x.Price).ToList();

            int productCount = lst.Count();
            int TotalPages =  (int) Math.Ceiling(productCount * 1.00 / PageSize);



            if (pageIndex > 1)
                lst = lst.Skip(PageSize * (pageIndex - 1)).Take(PageSize).ToList();
                
            

            ViewData["search"] = search;
            ViewData["category"] = cat;
            ViewData["brand"] = brand;
            ViewData["sorting"] = sorting;

            ViewData["PageIndex"] = pageIndex;
            ViewData["PageSize"] = PageSize;
            ViewData["TotalPages"] = TotalPages;

            ViewData["categories"] =  productService.GetAllCategories();
            ViewData["brands"] = productService.GetAllBrands();
            ViewData["sorts"] = productService.GetAllsortings();

            

            foreach (var item in lst)
            {
                bool isCarted = await unitOfWork.CartRepo.GetCartByProductAndUser(item.Id, userId);
                int id = item.Id;

                ViewBag.id = (isCarted) ? id : -1;
            }
            

            return View(lst);


        }

       

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
             var product = await unitOfWork.ProductRepo.GetByIdAsync(id);
             var UserId =  User.FindFirstValue(ClaimTypes.NameIdentifier);

             ViewData["isCarted"] = await unitOfWork.CartRepo.GetCartByProductAndUser(id, UserId);

             return View(product);
        }



       
        [HttpGet]
        public async Task<IActionResult> AddNewProduct()
        {
            var prodVM = await productService.IntializeProductVM();
            return View(prodVM);
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewProduct(ProductVM prod)
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

        /*
        [HttpPost]
        public async Task<IActionResult> SaveEdit(ProductCreateUpdateVM prod)
        {
            var product = await unitOfWork.ProductRepo.GetByIdAsync(id);
        }

        */

        public async Task<IActionResult> DeleteProduct(int id)
        {
            await unitOfWork.ProductRepo.deleteAsync(id);
            return RedirectToAction("Index");
        }



        
    }
}
