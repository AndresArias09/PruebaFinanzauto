namespace Domain.Dto.Cursos
{
    public class CrearCursoRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public long? IdProfesor { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
