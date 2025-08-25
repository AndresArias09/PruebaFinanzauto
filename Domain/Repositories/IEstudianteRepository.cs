using Domain.Dto;
using Domain.Dto.Estudiantes;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IEstudianteRepository : IGenericRepository<Estudiante>
    {
        public Task<Estudiante> GetEstudianteByNumDoc(string numDoc, CancellationToken cancellationToken);
        public Task<PaginatedCollection<EstudianteDto>> GetEstudiantesPaginado(int page, int pageSize, CancellationToken cancellationToken);
    }
}
