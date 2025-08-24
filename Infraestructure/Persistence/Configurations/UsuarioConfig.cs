using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasIndex(_ => _.DateAdded);
            builder.HasIndex(_ => _.NombreUsuario);

            builder.HasData(new List<Usuario>
            {
                new Usuario()
                {
                    Id = 1,
                    NombreUsuario = "admin",
                    Contraseña = "B9A465912169BEF97138C76EFDFD5BB34FDC5FA58855AC187817AE07E80ABE5E-5929B1B6239B2767DDEDDABC98823ADF", //123
                    DateAdded = new DateTime(2025,8,24)
                }
            });
        }
    }
}
