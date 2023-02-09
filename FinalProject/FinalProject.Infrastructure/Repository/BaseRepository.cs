using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Common;
using FinalProject.Infrastructure.Context;

namespace FinalProject.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : AuditableBaseEntity
    {
        private readonly AppDbContext context;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
