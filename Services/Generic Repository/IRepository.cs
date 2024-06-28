using System.Linq.Expressions;

namespace WebinarManagement.Services
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task RemoveAsync(T entity);
    }
}
