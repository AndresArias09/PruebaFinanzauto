using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProfesorRepository : IGenericRepository<Profesor>
    {
        public Task<Profesor> GetProfesorByNumDoc(string numDoc, CancellationToken cancellationToken);
    }
}
