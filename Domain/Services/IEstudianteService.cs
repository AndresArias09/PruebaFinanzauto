using Domain.Dto.EstudianteDto;
using Domain.Entities;

namespace Domain.Services
{
    public interface IEstudianteService
    {
        public Task<Result<EstudianteDto>> GuardarNuevoEstudiante(CrearEstudianteRequest request, CancellationToken cancellationToken);
        public Task<Result<EstudianteDto>> ActualizarInfoEstudiante(ActualizarEstudianteRequest request, CancellationToken cancellationToken);
        public Task<Result<bool>> EliminarEstudiante(long id, CancellationToken cancellationToken);
        public Task<IEnumerable<EstudianteDto>> ConsultarEstudiantes(CancellationToken cancellationToken);
        public Task<EstudianteDto> GetEstudianteById(long id, CancellationToken cancellationToken);
    }
}
