using Domain.Entities;

namespace Domain.Dto.Profesores
{
    public class ProfesorDto : CrearProfesorRequest
    {
        public long Id { get; set; }

        public static ProfesorDto GetFromModel(Profesor profesor)
        {
            return new ProfesorDto()
            {
                Id = profesor.Id,
                Nombres = profesor.Names,
                Apellidos = profesor.Surnames,
                Correo = profesor.Email,
                FechaIngreso = profesor.EntryDate,
                NumeroDocumento = profesor.DocumentId
            };
        }
    }
}
