using E_Commerce2.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace E_Commerce2.UnitOfWorkk
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepo ProductRepo { get; }
        ICartRepo CartRepo { get; }

        IOrderRepo OrderRepo { get; }

        IOrderItemRepo OrderItemRepo { get; }

        Task<int> Complete();
        Task<IDbContextTransaction> BeginTransaction();
    }
}
