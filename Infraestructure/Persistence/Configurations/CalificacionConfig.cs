using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class CalificacionConfig : IEntityTypeConfiguration<Calificacion>
    {
        public void Configure(EntityTypeBuilder<Calificacion> builder)
        {
            builder.ToTable("Calificacion");

            builder.Property(_ => _.Concept).HasColumnType("nvarchar(300)");
            builder.Property(_ => _.Value)
                .HasColumnType("decimal(18,2)");

            builder.HasIndex(_ => _.DateAdded);

            //CursoEstudiante

            builder.HasOne(_ => _.CursoEstudiante)
                .WithMany(_ => _.Calificaciones)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(_ => _.IdCursoEstudiante);
        }
    }
}
