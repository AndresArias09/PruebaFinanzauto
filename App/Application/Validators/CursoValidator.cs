using Domain.Dto.Cursos;
using FluentValidation;

namespace Application.Validators
{
    public class CursoValidator : AbstractValidator<CrearCursoRequest>
    {
        public CursoValidator()
        {
            RuleFor(_ => _.Nombre)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio");

            RuleFor(_ => _.FechaInicio)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("La fecha de inicio es obligatoria");

            RuleFor(_ => _.FechaFin)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("La fecha de fin es obligatoria");

            RuleFor(_ => _.IdProfesor)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Debe indicar el profesor asignado al curso");
        }
    }
}
