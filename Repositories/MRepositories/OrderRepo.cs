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

          
          


            var OrderWithItemsAndProduct = await dbset
                                                    .Include(o => o.Items)
                                                    .ThenInclude(i => i.Product)
                                                    .FirstOrDefaultAsync(o => o.Id == id);


            if (OrderWithItemsAndProduct != null)
            {
                Console.WriteLine("HHHHHH");
               // Console.WriteLine(OrderWithItemsAndProduct.Items[0].Product.ImageFileName);
                Console.WriteLine(OrderWithItemsAndProduct.Items == null);
                Console.WriteLine(OrderWithItemsAndProduct.Items[0].Product.Price );
                Console.WriteLine(OrderWithItemsAndProduct.Items[0].Product.Name + "444");
            }
                                     

            return OrderWithItemsAndProduct;
        }


        public async Task<List<Order>> GetOrdersByUserID(string id)
        {
            return await dbset.Where(o => o.CustomerId == id).Include(o => o.Items).ThenInclude(i => i.Product).ToListAsync();
        }
    }
}
