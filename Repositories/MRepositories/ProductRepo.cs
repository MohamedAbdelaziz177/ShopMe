using E_Commerce2.Data;
using E_Commerce2.Models;
using E_Commerce2.Repositories.IRepositories;

namespace E_Commerce2.Repositories.MRepositories
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        public ProductRepo(AppDbContext con) : base(con)
        {
        }
    }
}
