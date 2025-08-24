using Application.Validators;
using Domain.Dto.EstudianteDto;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudianteRepository _estudianteRepository;

        public EstudianteService(IEstudianteRepository estudianteRepository)
        {
            _estudianteRepository = estudianteRepository;
        }
        public async Task<Result<EstudianteDto>> GuardarNuevoEstudiante(CrearEstudianteRequest request, CancellationToken cancellationToken)
        {
            var validator = new EstudianteValidator();

            var resultValidation = validator.Validate(request);

            if (!resultValidation.IsValid)
            {
                return Result<EstudianteDto>.Failure(resultValidation.Errors.FirstOrDefault()!.ErrorMessage);
            }

            var estudiante = new Estudiante()
            {
                Names = request.Nombres,
                Surnames = request.Apellidos,
                Email = request.Correo,
                DocumentId = request.NumeroDocumento,
                EntryDate = request.FechaIngreso
            };

            estudiante = await _estudianteRepository.SaveInfo(estudiante, cancellationToken);

            return Result<EstudianteDto>.Success(EstudianteDto.GetFromModel(estudiante));
        }

        public async Task<Result<EstudianteDto>> ActualizarInfoEstudiante(ActualizarEstudianteRequest request, CancellationToken cancellationToken)
        {
            //Validar si existe

            var temp = await _estudianteRepository.GetById(request.Id, cancellationToken);

            if (temp is null)
            {
                throw new EntityNotFoundException("Registro no encontrado");
            }

            var validator = new EstudianteValidator();

            var resultValidation = validator.Validate(request);

            if (!resultValidation.IsValid)
            {
                return Result<EstudianteDto>.Failure(resultValidation.Errors.FirstOrDefault()!.ErrorMessage);
            }

            var estudiante = new Estudiante()
            {
                Id = request.Id,
                Names = request.Nombres,
                Surnames = request.Apellidos,
                Email = request.Correo,
                DocumentId = request.NumeroDocumento,
                EntryDate = request.FechaIngreso
            };

            estudiante = await _estudianteRepository.SaveInfo(estudiante, cancellationToken);

            return Result<EstudianteDto>.Success(EstudianteDto.GetFromModel(estudiante));
        }

        public async Task<IEnumerable<EstudianteDto>> ConsultarEstudiantes(CancellationToken cancellationToken)
        {
            var estudiantes = await _estudianteRepository.Get(cancellationToken);

            return estudiantes.Select(_ => new EstudianteDto()
            {
                Id = _.Id,
                NumeroDocumento = _.DocumentId,
                Nombres = _.Names,
                Apellidos = _.Surnames,
                Correo = _.Email,
                FechaIngreso = _.EntryDate
            });
        }

        public async Task<Result<bool>> EliminarEstudiante(long id, CancellationToken cancellationToken)
        {
            //Validar si existe

            var temp = await _estudianteRepository.GetById(id, cancellationToken);

            if (temp is null)
            {
                throw new EntityNotFoundException("Registro no encontrado");
            }

            await _estudianteRepository.DeleteById(id, cancellationToken);

            return Result<bool>.Success(true);
        }

        public async Task<EstudianteDto> GetEstudianteById(long id, CancellationToken cancellationToken)
        {
            var estudiante = await _estudianteRepository.GetById(id, cancellationToken);

            return EstudianteDto.GetFromModel(estudiante);
        }
    }
}
