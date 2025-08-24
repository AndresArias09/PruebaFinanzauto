using Domain.Dto.Profesores;
using Domain.Entities;

namespace Domain.Services
{
    public interface IProfesorService
    {
        public Task<Result<ProfesorDto>> GuardarNuevoProfesor(CrearProfesorRequest request, CancellationToken cancellationToken);
        public Task<Result<ProfesorDto>> ActualizarInfoProfesor(ActualizarProfesorRequest request, CancellationToken cancellationToken);
        public Task<Result<bool>> EliminarProfesor(long id, CancellationToken cancellationToken);
        public Task<IEnumerable<ProfesorDto>> ConsultarProfesors(CancellationToken cancellationToken);
        public Task<ProfesorDto> GetProfesorById(long id, CancellationToken cancellationToken);
        public Task<ProfesorDto> GetProfesorByNumDoc(string numDoc, CancellationToken cancellationToken);
    }
}
