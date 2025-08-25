using Domain.Dto.Calificaciones;
using Domain.Entities;

namespace Domain.Services
{
    public interface ICalificacionService
    {

        public Task<Result<bool>> AgregarCalificacionEstudiante(AgregarCalificacionRequest request, CancellationToken cancellationToken);
        public Task<Result<bool>> ModificarCalificacionEstudiante(ModificarCalificacionRequest request, CancellationToken cancellationToken);
        public Task<IEnumerable<CalificacionDto>> ConsultarCalificacionesByEstudianteYCurso(long idEstudiante, long idCurso, CancellationToken cancellationToken);
        public Task<Result<bool>> EliminarCalificacion(long id, CancellationToken cancellationToken);
    }
}
