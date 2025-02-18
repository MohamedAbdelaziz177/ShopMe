using E_Commerce2.Data;
using E_Commerce2.Models;
using E_Commerce2.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce2.Repositories.MRepositories
{
    public class OrderItemRepo : GenericRepo<OrderItem>, IOrderItemRepo
    {
        public OrderItemRepo(AppDbContext con) : base(con)
        {
        }

        public OrderItem GetItemWithName(int id)
        {
            return dbset.Include(o => o.Product).FirstOrDefault(o => o.Id == id);
        }
    }
}
