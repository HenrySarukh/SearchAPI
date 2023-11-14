using System.Linq.Expressions;

namespace SearchAPI.Domain.Contracts
{
    public interface IAsyncRepository<T>
        where T : class
    {
        Task<T> GetById(int id);
        Task<IReadOnlyList<T>> ListAll(Func<T, bool> filter = null);
        Task<IReadOnlyList<T>> List(int? take, int? skip, Expression<Func<T, bool>> filter = null);
        Task<T> Add(T entity, bool save = true);
        Task<ICollection<T>> AddRange(ICollection<T> entities, bool save = true);
        Task<bool> Update(T entity, bool save = true);
        Task<bool> UpdateRange(ICollection<T> entities, bool save = true);
        Task<bool> Delete(T entity, bool save = true);
        Task<bool> DeleteRange(ICollection<T> entities, bool save = true);
    }
}
