using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICalificacionRepository : IGenericRepository<Calificacion>
    {
        public Task<IEnumerable<Calificacion>> GetCalificacionesByEstudianteYCurso(long idEstudiante, long idCurso, CancellationToken cancellationToken);
    }
}
