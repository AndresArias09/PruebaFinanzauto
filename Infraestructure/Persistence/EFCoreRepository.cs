using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public abstract class EFCoreRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbContext;

        protected EFCoreRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<bool> DeleteById(long id, CancellationToken cancellationToken)
        {
            var entity = await GetById(id, cancellationToken);

            if (entity is null) return false;

            _dbContext.Set<T>().Remove(entity);

            int entities = await _dbContext.SaveChangesAsync(cancellationToken);

            return entities > 0;
        }

        public virtual async Task<IEnumerable<T>> Get(CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().ToListAsync(cancellationToken);
        }

        public virtual async Task<T> GetById(long id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(_ => _.Id == id, cancellationToken);
        }

        public virtual async Task<T> SaveInfo(T entity, CancellationToken cancellationToken)
        {
            var temp = await GetById(entity.Id, cancellationToken);

            if (temp is null)
            {
                entity.DateAdded = DateTime.Now;
                _dbContext.Set<T>().Add(entity);
            }
            else
            {
                entity.DateLastUpdate = DateTime.Now;
                _dbContext.Set<T>().Update(entity);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
