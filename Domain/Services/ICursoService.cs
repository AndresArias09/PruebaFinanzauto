using Domain.Dto.Cursos;
using Domain.Entities;

namespace Domain.Services
{
    public interface ICursoService
    {
        public Task<Result<CursoDto>> GuardarNuevoCurso(CrearCursoRequest request, CancellationToken cancellationToken);
        public Task<Result<CursoDto>> ActualizarInfoCurso(ActualizarCursoRequest request, CancellationToken cancellationToken);
        public Task<Result<bool>> EliminarCurso(long id, CancellationToken cancellationToken);
        public Task<IEnumerable<CursoDto>> ConsultarCursos(CancellationToken cancellationToken);
        public Task<CursoDto> GetCursoById(long id, CancellationToken cancellationToken);
        public Task<Result<bool>> MatricularEstudianteCurso(MatriculacionRequest request, CancellationToken cancellationToken);
        public Task<Result<bool>> DesmatricularEstudianteCurso(MatriculacionRequest request, CancellationToken cancellationToken);

    }
}
