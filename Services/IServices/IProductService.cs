using E_Commerce2.Models;
using E_Commerce2.ViewModels.ProductVM_s;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce2.Services.IServices
{
    public interface IProductService
    {
         Task<string> CreateImgUrl(IFormFile img);
         Task SaveProduct(ProductVM prod);

         Task<ProductVM> IntializeProductVM();

        Task<ProductVM> MapProductModelToVM(Product product);

        List<SelectListItem> GetAllCategories();

        List<SelectListItem> GetAllBrands();

        List<SelectListItem> GetAllsortings();




    }
}
