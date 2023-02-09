using FinalProject.Core.Common;

namespace FinalProject.Application.Common.Interfaces
{
    public interface IBaseRepository<T> where T : AuditableBaseEntity
    {
        Task<int> Add(T entity);
    }
}
