using E_Commerce2.Data;
using E_Commerce2.Repositories.IRepositories;
using E_Commerce2.Repositories.MRepositories;

namespace E_Commerce2.UnitOfWorkk
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext con;
        public IProductRepo ProductRepo {  get; private set; }
        public ICartRepo CartRepo { get; private set; }

        public UnitOfWork(AppDbContext con)
        {
            this.con = con;
            ProductRepo = new ProductRepo(con);
            CartRepo = new CartRepo(con);

        }

        public async Task<int> Complete()
        {
            return await con.SaveChangesAsync();
        }
        public void Dispose() 
        {
            con.Dispose();
        }
    }
}
