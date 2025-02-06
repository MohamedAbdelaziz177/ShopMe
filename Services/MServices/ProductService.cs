using E_Commerce2.Models;
using E_Commerce2.Services.IServices;
using E_Commerce2.UnitOfWorkk;
using E_Commerce2.ViewModels.ProductVM_s;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce2.Services.MServices
{
    public class ProductService : IProductService 
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> CreateImgUrl(IFormFile img)
        {
            string imgName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
            string folder = Path.Combine(webHostEnvironment.WebRootPath, "Images", "products");

            string fullImgPath = Path.Combine(folder, imgName);

            using(var fileStream = new FileStream(fullImgPath, FileMode.Create))
            {
                await img.CopyToAsync(fileStream);
            }

            return imgName;
        }


       public async Task SaveProduct(ProductVM prod)
       {
            string imgUrl = await CreateImgUrl(prod.ImageFile);

            Product product = new Product()
            {
                ImageFileName = imgUrl,
                Name = prod.Name,
                Category = prod.Category,
                Brand = prod.Brand,
                Description = prod.Description,
                Price = prod.Price,
                CreatedAt = DateTime.Now
            };

            await unitOfWork.ProductRepo.insertAsync(product);
        }


        public List<SelectListItem> GetAllCategories()
        {
            var cats = new List<SelectListItem>(){
                    new SelectListItem { Text = "Other", Value = "Other"},
                    new SelectListItem { Text = "Phones", Value = "Phones"},
                    new SelectListItem { Text = "Computers", Value = "Computers"},
                    new SelectListItem { Text = "Accessories", Value = "Accessories"},
                    new SelectListItem { Text = "Printers", Value = "Printers"},
                    new SelectListItem { Text = "Cameras", Value = "Cameras"}

                };

            return cats;
        }

        public  List<SelectListItem> GetAllBrands()
        {
            var brands = new List<SelectListItem>(){
                    new SelectListItem { Text = "Samsung", Value = "Samsung"},
                    new SelectListItem { Text = "Apple", Value = "Apple"},
                    new SelectListItem { Text = "Nokia", Value = "Nokia"},
                    new SelectListItem { Text = "HP", Value = "HP"},
                    new SelectListItem { Text = "All Brands", Value = "All Brands"}


                };

            return brands;
        }


        public List<SelectListItem> GetAllsortings()
        {
            var sorts = new List<SelectListItem>(){
                    new SelectListItem { Text = "Order By Newest", Value = "newest"},
                    new SelectListItem { Text = "Price: Low to High", Value = "price_asc"},
                    new SelectListItem { Text = "Price: High to Low", Value = "price_desc"}

                };


            return sorts;
        }




        public async Task<ProductVM> IntializeProductVM()
        {
            var cats = new List<SelectListItem>(){
                    new SelectListItem { Text = "Other", Value = "Other"},
                    new SelectListItem { Text = "Phones", Value = "Phones"},
                    new SelectListItem { Text = "Computers", Value = "Computers"},
                    new SelectListItem { Text = "Accessories", Value = "Accessories"},
                    new SelectListItem { Text = "Printers", Value = "Printers"},
                    new SelectListItem { Text = "Cameras", Value = "Cameras"}

                };



            ProductVM prod = new ProductVM() { categories = cats};

            return prod;
        }

        public async Task<ProductVM> MapProductModelToVM(Product product)
        {
           
            var prod = await IntializeProductVM();
          //  string imgUrl = await CreateImgUrl(prod.ImageFile);

            prod.Brand = product.Brand;
            prod.Description = product.Description;
            prod.Price = product.Price;
            prod.CreatedAt = product.CreatedAt;
            prod.Category = product.Category;
            prod.ImageFileName = product.ImageFileName;
            prod.Name = product.Name;
            
            return prod;
        }
    }
}
