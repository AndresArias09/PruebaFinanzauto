using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class EstudianteConfig : IEntityTypeConfiguration<Estudiante>
    {
        public void Configure(EntityTypeBuilder<Estudiante> builder)
        {
            builder.ToTable("Estudiante");

            builder.HasIndex(_ => _.DateAdded);
            builder.HasIndex(_ => _.EntryDate);
            builder.HasIndex(_ => _.DocumentId)
                .IsUnique();

            builder.HasData(new List<Estudiante>()
            {
                new Estudiante()
                {
                    Id = 1,
                    DateAdded = new DateTime(2025,8,24),
                    DocumentId = "99888923",
                    Names = "Andrés Leonardo",
                    Surnames = "Arias Uribe",
                    EntryDate = DateTime.Now.Date,
                    Email = "example@domain.co"
                },
                new Estudiante()
                {
                    Id = 2,
                    DateAdded = new DateTime(2025,8,24),
                    DocumentId = "998889223",
                    Names = "David Alfonso",
                    Surnames = "Cárdenas Suarez",
                    EntryDate = DateTime.Now.Date,
                    Email = "example@domain.co"
                }
            });
        }
    }
}
