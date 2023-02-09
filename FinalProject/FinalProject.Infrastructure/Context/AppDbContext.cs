using FinalProject.Core.Entities;
using FinalProject.Infrastructure.Identity;
using FinalProject.Infrastructure.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infrastructure.Context
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        private readonly AuditableEntitySaveChangesInterceptor interceptor;

        public AppDbContext(DbContextOptions<AppDbContext> options,
            AuditableEntitySaveChangesInterceptor interceptor)
            : base(options)
        {
            this.interceptor = interceptor;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(interceptor);
        }
    }
}
