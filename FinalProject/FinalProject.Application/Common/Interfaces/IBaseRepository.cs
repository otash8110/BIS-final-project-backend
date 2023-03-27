using FinalProject.Core.Common;
using System.Linq.Expressions;

namespace FinalProject.Application.Common.Interfaces
{
    public interface IBaseRepository<T> where T : AuditableBaseEntity
    {
        Task<IList<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> filter);
        Task<int> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<T> GetOneAsync(int id);
    }
}
