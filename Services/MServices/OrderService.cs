using E_Commerce2.Models;
using E_Commerce2.Services.IServices;
using E_Commerce2.UnitOfWorkk;
using E_Commerce2.ViewModels.CartVM_s;
using E_Commerce2.ViewModels.OderVM_s;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce2.Services.MServices
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateOrder(OrderVM order, string UserId, decimal subTotal)
        {
            Order newOrder = new Order();

            newOrder.DeliveryAddress = order.DeliveryAddress;
            newOrder.ShippingFee = 12.5M;
            newOrder.PaymentMethod = order.PaymentMethod;
            newOrder.CreatedAt = DateTime.Now;
            newOrder.CustomerId = UserId;
            newOrder.OrderStatus = "Pending";
            newOrder.SubTotal = subTotal;

            await unitOfWork.OrderRepo.insertAsync(newOrder);

            return newOrder;
        }


        public async Task<List<OrderItem>> InsertOrderItems(List<CartVM> cartVMs)
        {
            List<OrderItem> items = new List<OrderItem>();
            foreach (var cartItem in cartVMs)
            {
                OrderItem item = new OrderItem();
                item.UnitPrice = cartItem.Price;
                item.Quantity = cartItem.Quantity;
                item.ProductId = cartItem.ProductId;

                items.Add(item);
                await unitOfWork.OrderItemRepo.insertAsync(item);

            }

            return items;
        }
    }
}
