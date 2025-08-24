namespace Domain.Entities
{
    public class Estudiante : BaseEntity
    {
        public string? DocumentId { get; set; }
        public string? Names { get; set; }
        public string? Surnames { get; set; }
        public string? Email { get; set; }
        public DateTime? EntryDate { get; set; }
        public virtual List<CursoEstudiante> Matriculas { get; set; } = new();
    }
}
