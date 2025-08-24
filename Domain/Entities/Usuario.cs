using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Usuario")]
    public class Usuario : BaseEntity
    {
        public string? NombreUsuario { get; set; }
        public string? Contraseña { get; set; }
    }
}
