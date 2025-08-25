using Domain.Entities;

namespace Domain.Dto.Cursos
{
    public class CursoDto : CrearCursoRequest
    {
        public long Id { get; set; }


        public static CursoDto GetFromModel(Curso curso)
        {
            return new CursoDto()
            {
                Id = curso.Id,
                Nombre = curso.Name,
                IdProfesor = curso.IdProfesor,
                FechaInicio = curso.StartDate,
                FechaFin = curso.EndDate
            };
        }
    }
}
