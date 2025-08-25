namespace Domain.Dto.Calificaciones
{
    public class AgregarCalificacionRequest
    {
        public long? IdCurso { get; set; }
        public long? IdEstudiante { get; set; }
        public decimal? Valor { get; set; }
        public string Concepto { get; set; } = string.Empty;
    }
}
