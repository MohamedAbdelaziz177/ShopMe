using E_Commerce2.Data;
using E_Commerce2.Models;
using E_Commerce2.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce2.Repositories.MRepositories
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        public OrderRepo(AppDbContext con) : base(con)
        {
        }

        public async Task<Order> GetOrderItemsByOrderId(int id)
        {
            var exists = await dbset.AnyAsync(o => o.Id == id);
            Console.WriteLine($"Order Exists: {exists}");

            var OrderWithItemsAndProduct = await dbset.Where(o => o.Id == id)
                                                      .Include(o => o.Items)
                                                      .ThenInclude(o => o.Product)
                                                       .FirstOrDefaultAsync();
            if(OrderWithItemsAndProduct != null)
                Console.WriteLine("HHHHHH");
                                     

            return OrderWithItemsAndProduct;
        }
    }
}
