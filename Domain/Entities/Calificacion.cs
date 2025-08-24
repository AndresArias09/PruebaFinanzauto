namespace Domain.Entities
{
    public class Calificacion : BaseEntity
    {
        public long? IdCursoEstudiante { get; set; }
        public virtual CursoEstudiante? CursoEstudiante { get; set; }
        public decimal? Value { get; set; }
        public string? Concept { get; set; }
    }
}
