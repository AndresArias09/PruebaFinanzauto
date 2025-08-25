namespace Domain.Dto.Calificaciones
{
    public class ModificarCalificacionRequest
    {
        public long Id { get; set; }
        public decimal? Valor { get; set; }
        public string Concepto { get; set; } = string.Empty;
    }
}
