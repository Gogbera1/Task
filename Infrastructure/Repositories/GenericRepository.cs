using Common.BaseModel;
using Common.Repository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IdentityDbContext _dbContext;
        protected GenericRepository(IdentityDbContext dbContext) => _dbContext = dbContext;
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            var entry = await DbSet.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }

        public async Task<int> SoftDeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity is not ISoftDeletable e) throw new ArgumentException($"{typeof(T).Name} is not 'SoftDeletable'");
            e.IsDeleted = true;
            DbSet.Update(entity);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            var entry = DbSet.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }
        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(expression, cancellationToken);
        }
        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }
        private DbSet<T> DbSet => _dbContext.Set<T>();

    }
}
