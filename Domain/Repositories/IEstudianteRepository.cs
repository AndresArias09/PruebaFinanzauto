using Domain.Entities;

namespace Domain.Repositories
{
    public interface IEstudianteRepository : IGenericRepository<Estudiante>
    {
        public Task<Estudiante> GetEstudianteByNumDoc(string numDoc, CancellationToken cancellationToken);
    }
}
