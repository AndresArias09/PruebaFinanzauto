namespace Domain.Entities
{
    public class Profesor : BaseEntity
    {
        public string? DocumentId { get; set; }
        public string? Names { get; set; }
        public string? Surnames { get; set; }
        public string? Email { get; set; }
        public DateTime? EntryDate { get; set; }
        public virtual List<Curso> Cursos { get; set; } = new();
    }
}
