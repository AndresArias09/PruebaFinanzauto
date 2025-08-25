using Domain.Dto.Calificaciones;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services
{
    public class CalificacionService : ICalificacionService
    {
        private readonly ICalificacionRepository _calificacionRepository;
        private readonly ICursoRepository _cursoRepository;
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly ICursoEstudianteRepository _cursoEstudianteRepository;

        public CalificacionService
        (
            ICalificacionRepository calificacionRepository,
            ICursoRepository cursoRepository,
            IEstudianteRepository estudianteRepository,
            ICursoEstudianteRepository cursoEstudianteRepository
        )
        {
            _calificacionRepository = calificacionRepository;
            _cursoRepository = cursoRepository;
            _estudianteRepository = estudianteRepository;
            _cursoEstudianteRepository = cursoEstudianteRepository;
        }

        public async Task<Result<bool>> AgregarCalificacionEstudiante(AgregarCalificacionRequest request, CancellationToken cancellationToken)
        {
            if (request.IdEstudiante is null)
            {
                return Result<bool>.Failure("Estudiante no válido");
            }

            if (request.IdCurso is null)
            {
                return Result<bool>.Failure("Curso no válido");
            }

            if (request.Valor is null)
            {
                return Result<bool>.Failure("Nota no válida");
            }

            if (request.Valor!.Value < 0)
            {
                return Result<bool>.Failure("La nota debe ser mayor a cero");
            }

            var estudiante = await _estudianteRepository.GetById(request.IdEstudiante!.Value, cancellationToken);

            if (estudiante is null)
            {
                throw new EntityNotFoundException("Estudiante no encontrado");
            }

            var curso = await _cursoRepository.GetById(request.IdCurso!.Value, cancellationToken);

            if (curso is null)
            {
                throw new EntityNotFoundException("Curso no encontrado");
            }

            var matricula = await _cursoEstudianteRepository.GetMatricula(request.IdEstudiante!.Value, request.IdCurso!.Value, cancellationToken);

            if (matricula is null)
            {
                throw new EntityNotFoundException("Matrícula no válida");
            }

            var calificacion = new Calificacion()
            {
                Concept = request.Concepto,
                Value = request.Valor,
                IdCursoEstudiante = matricula.Id
            };

            calificacion = await _calificacionRepository.SaveInfo(calificacion, cancellationToken);

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> ModificarCalificacionEstudiante(ModificarCalificacionRequest request, CancellationToken cancellationToken)
        {
            if (request.Valor is null)
            {
                return Result<bool>.Failure("Nota no válida");
            }

            if (request.Valor!.Value < 0)
            {
                return Result<bool>.Failure("La nota debe ser mayor a cero");
            }

            var calificacion = await _calificacionRepository.GetById(request.Id, cancellationToken);

            if (calificacion is null)
            {
                throw new EntityNotFoundException("Calificación no encontrada");
            }

            calificacion.Value = request.Valor;
            calificacion.Concept = request.Concepto;

            await _calificacionRepository.SaveInfo(calificacion, cancellationToken);

            return Result<bool>.Success(true);
        }

        public async Task<IEnumerable<CalificacionDto>> ConsultarCalificacionesByEstudianteYCurso(long idEstudiante, long idCurso, CancellationToken cancellationToken)
        {
            var calificaciones = await _calificacionRepository.GetCalificacionesByEstudianteYCurso(idEstudiante, idCurso, cancellationToken);

            return calificaciones.Select(_ => new CalificacionDto()
            {
                Id = _.Id,
                Concepto = _.Concept,
                IdCurso = idCurso,
                IdEstudiante = idEstudiante,
                Valor = _.Value
            });
        }

        public async Task<Result<bool>> EliminarCalificacion(long id, CancellationToken cancellationToken)
        {
            //Validar si existe

            var temp = await _calificacionRepository.GetById(id, cancellationToken);

            if (temp is null)
            {
                throw new EntityNotFoundException("Registro no encontrado");
            }

            await _calificacionRepository.DeleteById(id, cancellationToken);

            return Result<bool>.Success(true);
        }


    }
}
