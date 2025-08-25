namespace Domain.Entities
{
    public class Curso : BaseEntity
    {
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? IdProfesor { get; set; }
        public virtual Profesor? Profesor { get; set; }
        public virtual List<CursoEstudiante> Matriculas { get; set; } = new();
    }
}
