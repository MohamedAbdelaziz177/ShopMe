using E_Commerce2.Data;
using E_Commerce2.Models;
using E_Commerce2.Repositories.IRepositories;

namespace E_Commerce2.Repositories.MRepositories
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        public OrderRepo(AppDbContext con) : base(con)
        {
        }
    }
}
