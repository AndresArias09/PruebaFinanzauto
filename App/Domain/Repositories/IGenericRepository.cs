using Domain.Entities;

namespace Domain.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<T> GetById(long id, CancellationToken cancellationToken);
        public Task<T> SaveInfo(T entity, CancellationToken cancellationToken);
        public Task<IEnumerable<T>> Get(CancellationToken cancellationToken);
        public Task<bool> DeleteById(long id, CancellationToken cancellationToken);
    }
}
