using FinalProject.Core.Common;
using System.Linq.Expressions;

namespace FinalProject.Application.Common.Interfaces
{
    public interface IBaseRepository<T> where T : AuditableBaseEntity
    {
        Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> filter);
        Task<int> Add(T entity);
    }
}
