using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICursoEstudianteRepository : IGenericRepository<CursoEstudiante>
    {
        public Task<bool> EliminarMatriculacionEstudianteCurso(long idEstudiante, long idCurso, CancellationToken cancellationToken);
        public Task<CursoEstudiante> GetMatricula(long idEstudiante, long idCurso, CancellationToken cancellationToken);
    }
}
