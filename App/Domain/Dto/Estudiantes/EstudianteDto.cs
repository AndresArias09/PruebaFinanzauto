using Domain.Entities;

namespace Domain.Dto.Estudiantes
{
    public class EstudianteDto : CrearEstudianteRequest
    {
        public long Id { get; set; }

        public static EstudianteDto GetFromModel(Estudiante estudiante)
        {
            return new EstudianteDto()
            {
                Id = estudiante.Id,
                Nombres = estudiante.Names,
                Apellidos = estudiante.Surnames,
                Correo = estudiante.Email,
                FechaIngreso = estudiante.EntryDate,
                NumeroDocumento = estudiante.DocumentId
            };
        }
    }
}
