namespace E_Commerce2.Repositories.IRepositories
{
    public interface IGenericRepo<T> where T : class
    {
        Task insertAsync(T entity);
        Task<bool> deleteAsync(int id);
        Task updateAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
    }
}
