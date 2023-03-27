using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Common;
using FinalProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<IList<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> filter)
        {
            var result = await context.Set<T>().Where(filter).ToListAsync();
            return result;
        }

        public async Task<T> GetOne(int id)
        {
            return await context.Set<T>().Where(i => i.Id == id).FirstAsync();
        }

        public async Task<bool> Update(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
