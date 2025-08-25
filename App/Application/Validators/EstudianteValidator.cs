using Domain.Constants;
using Domain.Dto.Estudiantes;
using FluentValidation;

namespace Application.Validators
{
    public class EstudianteValidator : AbstractValidator<CrearEstudianteRequest>
    {
        public EstudianteValidator()
        {
            RuleFor(_ => _.NumeroDocumento)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("El número de documento es obligatorio");


            RuleFor(_ => _.Nombres)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Los nombres son obligatorios");


            RuleFor(_ => _.Apellidos)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage("Los apellidos son obligatorios");

            RuleFor(_ => _.Correo)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage("El correo electrónico es obligatorio")
               .Matches(RegexConstants.EMAIL_REGEX)
               .WithMessage("Formato de correo no válido");

            RuleFor(_ => _.FechaIngreso)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage("La fecha de ingreso es obligatoria");
        }
    }
}
