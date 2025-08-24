using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class CursoConfig : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");
            builder.Property(_ => _.Name).HasColumnType("nvarchar(200)");

            builder.HasIndex(_ => _.StartDate);

            //Profesor
            builder.HasOne(_ => _.Profesor)
                .WithMany(_ => _.Cursos)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(_ => _.IdProfesor);
        }
    }
}
