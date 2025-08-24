namespace Domain.Entities
{
    public class CursoEstudiante : BaseEntity
    {
        public long? IdEstudiante { get; set; }
        public virtual Estudiante? Estudiante { get; set; }

        public long? IdCurso { get; set; }
        public virtual Curso? Curso { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        public virtual List<Calificacion> Calificaciones { get; set; } = new();
    }
}
