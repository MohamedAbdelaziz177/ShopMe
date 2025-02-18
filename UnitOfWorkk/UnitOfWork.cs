using E_Commerce2.Data;
using E_Commerce2.Repositories.IRepositories;
using E_Commerce2.Repositories.MRepositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace E_Commerce2.UnitOfWorkk
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext con;
        public IProductRepo ProductRepo {  get; private set; }
        public ICartRepo CartRepo { get; private set; }

        public IOrderRepo OrderRepo { get; private set; }

        public IOrderItemRepo OrderItemRepo { get; private set; }

        public UnitOfWork(AppDbContext con)
        {
            this.con = con;
            ProductRepo = new ProductRepo(con);
            CartRepo = new CartRepo(con);
            OrderRepo = new OrderRepo(con);
            OrderItemRepo = new OrderItemRepo(con);
        }

        public async Task<int> Complete()
        {
            return await con.SaveChangesAsync();
        }
        public void Dispose() 
        {
            con.Dispose();
        }

        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await con.Database.BeginTransactionAsync();
        }
    }
}
