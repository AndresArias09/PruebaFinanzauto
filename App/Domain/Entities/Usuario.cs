using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Usuario")]
    public class Usuario : BaseEntity
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
