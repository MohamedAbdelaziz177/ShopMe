using E_Commerce2.Repositories.IRepositories;

namespace E_Commerce2.UnitOfWorkk
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepo ProductRepo { get; }

        Task<int> Complete();
    }
}
