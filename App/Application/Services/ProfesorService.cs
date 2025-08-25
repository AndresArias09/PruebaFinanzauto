using Application.Validators;
using Domain.Dto.Profesores;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly IProfesorRepository _ProfesorRepository;

        public ProfesorService(IProfesorRepository ProfesorRepository)
        {
            _ProfesorRepository = ProfesorRepository;
        }
        public async Task<Result<ProfesorDto>> GuardarNuevoProfesor(CrearProfesorRequest request, CancellationToken cancellationToken)
        {
            var temp = await _ProfesorRepository.GetProfesorByNumDoc(request.NumeroDocumento, cancellationToken);

            if (temp is not null)
            {
                return Result<ProfesorDto>.Failure("Ya existe un profesor con el número de documento indicado");
            }

            var validator = new ProfesorValidator();

            var resultValidation = validator.Validate(request);

            if (!resultValidation.IsValid)
            {
                return Result<ProfesorDto>.Failure(resultValidation.Errors.FirstOrDefault()!.ErrorMessage);
            }

            var profesor = new Profesor()
            {
                Names = request.Nombres,
                Surnames = request.Apellidos,
                Email = request.Correo,
                DocumentId = request.NumeroDocumento,
                EntryDate = request.FechaIngreso
            };

            profesor = await _ProfesorRepository.SaveInfo(profesor, cancellationToken);

            return Result<ProfesorDto>.Success(ProfesorDto.GetFromModel(profesor));
        }

        public async Task<Result<ProfesorDto>> ActualizarInfoProfesor(ActualizarProfesorRequest request, CancellationToken cancellationToken)
        {
            //Validar si existe

            var temp = await _ProfesorRepository.GetById(request.Id, cancellationToken);

            if (temp is null)
            {
                throw new EntityNotFoundException("Registro no encontrado");
            }

            //Validar que no se repita el número de documento
            temp = await _ProfesorRepository.GetProfesorByNumDoc(request.NumeroDocumento, cancellationToken);

            if (temp is not null && temp.Id != request.Id)
            {
                return Result<ProfesorDto>.Failure("Ya existe un Profesor con el número de documento indicado");
            }

            var validator = new ProfesorValidator();

            var resultValidation = validator.Validate(request);

            if (!resultValidation.IsValid)
            {
                return Result<ProfesorDto>.Failure(resultValidation.Errors.FirstOrDefault()!.ErrorMessage);
            }

            var profesor = new Profesor()
            {
                Id = request.Id,
                Names = request.Nombres,
                Surnames = request.Apellidos,
                Email = request.Correo,
                DocumentId = request.NumeroDocumento,
                EntryDate = request.FechaIngreso
            };

            profesor = await _ProfesorRepository.SaveInfo(profesor, cancellationToken);

            return Result<ProfesorDto>.Success(ProfesorDto.GetFromModel(profesor));
        }

        public async Task<IEnumerable<ProfesorDto>> ConsultarProfesors(CancellationToken cancellationToken)
        {
            var Profesors = await _ProfesorRepository.Get(cancellationToken);

            return Profesors.Select(_ => new ProfesorDto()
            {
                Id = _.Id,
                NumeroDocumento = _.DocumentId,
                Nombres = _.Names,
                Apellidos = _.Surnames,
                Correo = _.Email,
                FechaIngreso = _.EntryDate
            });
        }

        public async Task<Result<bool>> EliminarProfesor(long id, CancellationToken cancellationToken)
        {
            //Validar si existe

            var temp = await _ProfesorRepository.GetById(id, cancellationToken);

            if (temp is null)
            {
                throw new EntityNotFoundException("Registro no encontrado");
            }

            await _ProfesorRepository.DeleteById(id, cancellationToken);

            return Result<bool>.Success(true);
        }

        public async Task<ProfesorDto> GetProfesorById(long id, CancellationToken cancellationToken)
        {
            var profesor = await _ProfesorRepository.GetById(id, cancellationToken);

            if (profesor is null) throw new EntityNotFoundException("No existe el registro");

            return ProfesorDto.GetFromModel(profesor);
        }

        public async Task<ProfesorDto> GetProfesorByNumDoc(string numDoc, CancellationToken cancellationToken)
        {
            var profesor = await _ProfesorRepository.GetProfesorByNumDoc(numDoc, cancellationToken);

            if (profesor is null) throw new EntityNotFoundException("No existe el registro");

            return ProfesorDto.GetFromModel(profesor);
        }
    }
}
