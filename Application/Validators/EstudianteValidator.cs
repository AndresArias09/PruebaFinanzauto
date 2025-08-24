using Domain.Dto.EstudianteDto;
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
               .WithMessage("El correo electrónico es obligatorio");

            RuleFor(_ => _.FechaIngreso)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage("La fecha de ingreso es obligatoria");
        }
    }
}
