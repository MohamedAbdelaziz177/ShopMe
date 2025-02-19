using E_Commerce2.Models;

namespace E_Commerce2.Repositories.IRepositories
{
    public interface IOrderRepo : IGenericRepo<Order>
    {
          Task<Order> GetOrderItemsByOrderId(int id);
          Task<List<Order>> GetOrdersByUserID(string id);
    }
}
