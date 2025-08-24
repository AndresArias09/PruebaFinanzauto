namespace Domain.Dto.EstudianteDto
{
    public class CrearEstudianteRequest
    {
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public DateTime? FechaIngreso { get; set; }
    }
}
