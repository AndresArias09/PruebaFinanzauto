using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class CursoEstudianteConfig : IEntityTypeConfiguration<CursoEstudiante>
    {
        public void Configure(EntityTypeBuilder<CursoEstudiante> builder)
        {
            builder.ToTable("CursoEstudiante");

            builder.HasIndex(_ => _.DateAdded);
            builder.HasIndex(_ => _.EnrollmentDate);

            //Curso
            builder.HasOne(_ => _.Curso)
                .WithMany(_ => _.Matriculas)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(_ => _.IdCurso);

            //Estudiante
            builder.HasOne(_ => _.Estudiante)
                .WithMany(_ => _.Matriculas)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(_ => _.IdEstudiante);
        }
    }
}
