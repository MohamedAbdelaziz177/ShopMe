using E_Commerce2.Models;
using E_Commerce2.ViewModels.CartVM_s;
using E_Commerce2.ViewModels.OderVM_s;

namespace E_Commerce2.Services.IServices
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(OrderVM order, string UserId, decimal subTotal);
        Task<List<OrderItem>> InsertOrderItems(List<CartVM> cartVMs, int orderId);
    }
}
