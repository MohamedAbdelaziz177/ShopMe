using E_Commerce2.Models;
using E_Commerce2.ViewModels.ProductVM_s;

namespace E_Commerce2.Services.IServices
{
    public interface IProductService
    {
         Task<string> CreateImgUrl(IFormFile img);
         Task SaveProduct(ProductCreateUpdateVM prod);

         Task<ProductCreateUpdateVM> IntializeProductVM();

        public Task<ProductCreateUpdateVM> MapProductModelToVM(Product product);

    }
}
