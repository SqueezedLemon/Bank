namespace Bank.Application.Contracts.Repositories
{
    public interface IGenericRepo<T> where T : class
    {
        Task<T> Get(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<bool> Exists(int id);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
