using Domain.Dto.Calificaciones;
using Domain.Entities;

namespace Domain.Services
{
    public interface ICalificacionService
    {

        public Task<Result<bool>> AgregarCalificacionEstudiante(AgregarCalificacionRequest request);
        public Task<Result<bool>> ModificarCalificacionEstudiante(AgregarCalificacionRequest request);
        public Task<IEnumerable<CalificacionDto>> ConsultarCalificacionesByEstudianteYCurso(long idEstudiante, long idCurso);
        public Task<Result<bool>> EliminarCalificacion(long id);
    }
}
