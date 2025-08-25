using Application.Validators;
using Domain.Dto.Cursos;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly ICursoEstudianteRepository _cursoEstudianteRepository;

        public CursoService
        (
            ICursoRepository cursoRepository,
            IEstudianteRepository estudianteRepository,
            ICursoEstudianteRepository cursoEstudianteRepository
        )
        {
            _cursoRepository = cursoRepository;
            _estudianteRepository = estudianteRepository;
            _cursoEstudianteRepository = cursoEstudianteRepository;
        }

        public async Task<Result<CursoDto>> GuardarNuevoCurso(CrearCursoRequest request, CancellationToken cancellationToken)
        {
            var validator = new CursoValidator();

            var resultValidation = validator.Validate(request);

            if (!resultValidation.IsValid)
            {
                return Result<CursoDto>.Failure(resultValidation.Errors.FirstOrDefault()!.ErrorMessage);
            }

            var curso = new Curso()
            {
                Name = request.Nombre,
                StartDate = request.FechaInicio,
                EndDate = request.FechaFin,
                IdProfesor = request.IdProfesor
            };

            curso = await _cursoRepository.SaveInfo(curso, cancellationToken);

            return Result<CursoDto>.Success(CursoDto.GetFromModel(curso));
        }

        public async Task<Result<CursoDto>> ActualizarInfoCurso(ActualizarCursoRequest request, CancellationToken cancellationToken)
        {
            //Validar si existe

            var temp = await _cursoRepository.GetById(request.Id, cancellationToken);

            if (temp is null)
            {
                throw new EntityNotFoundException("Registro no encontrado");
            }

            var validator = new CursoValidator();

            var resultValidation = validator.Validate(request);

            if (!resultValidation.IsValid)
            {
                return Result<CursoDto>.Failure(resultValidation.Errors.FirstOrDefault()!.ErrorMessage);
            }

            var curso = new Curso()
            {
                Id = request.Id,
                Name = request.Nombre,
                StartDate = request.FechaInicio,
                EndDate = request.FechaFin,
                IdProfesor = request.IdProfesor
            };

            curso = await _cursoRepository.SaveInfo(curso, cancellationToken);

            return Result<CursoDto>.Success(CursoDto.GetFromModel(curso));
        }

        public async Task<IEnumerable<CursoDto>> ConsultarCursos(CancellationToken cancellationToken)
        {
            var cursos = await _cursoRepository.Get(cancellationToken);

            return cursos.Select(_ => CursoDto.GetFromModel(_));
        }
        public async Task<CursoDto> GetCursoById(long id, CancellationToken cancellationToken)
        {
            var curso = await _cursoRepository.GetById(id, cancellationToken);

            return CursoDto.GetFromModel(curso);
        }
        public async Task<Result<bool>> EliminarCurso(long id, CancellationToken cancellationToken)
        {
            //Validar si existe

            var temp = await _cursoRepository.GetById(id, cancellationToken);

            if (temp is null)
            {
                throw new EntityNotFoundException("Registro no encontrado");
            }

            await _cursoRepository.DeleteById(id, cancellationToken);

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DesmatricularEstudianteCurso(MatriculacionRequest request, CancellationToken cancellationToken)
        {
            if (request.IdEstudiante is null)
            {
                return Result<bool>.Failure("Estudiante no válido");
            }

            if (request.IdCurso is null)
            {
                return Result<bool>.Failure("Curso no válido");
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

            await _cursoEstudianteRepository.EliminarMatriculacionEstudianteCurso(request.IdEstudiante!.Value, request.IdCurso!.Value, cancellationToken);

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> MatricularEstudianteCurso(MatriculacionRequest request, CancellationToken cancellationToken)
        {
            if (request.IdEstudiante is null)
            {
                return Result<bool>.Failure("Estudiante no válido");
            }

            if (request.IdCurso is null)
            {
                return Result<bool>.Failure("Curso no válido");
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

            var temp = await _cursoEstudianteRepository.GetMatricula(request.IdEstudiante!.Value, request.IdCurso!.Value, cancellationToken);

            if (temp is not null)
            {
                return Result<bool>.Failure("El estudiante ya está matriculado en el curso indicado");
            }

            var matriculacion = new CursoEstudiante()
            {
                IdCurso = request.IdCurso,
                IdEstudiante = request.IdEstudiante,
                EnrollmentDate = DateTime.Now
            };

            await _cursoEstudianteRepository.SaveInfo(matriculacion, cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
