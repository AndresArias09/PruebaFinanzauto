using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class ProfesorConfig : IEntityTypeConfiguration<Profesor>
    {
        public void Configure(EntityTypeBuilder<Profesor> builder)
        {
            builder.ToTable("Profesor");

            builder.Property(_ => _.DocumentId).HasColumnType("nvarchar(200)");
            builder.Property(_ => _.Names).HasColumnType("nvarchar(400)");
            builder.Property(_ => _.Surnames).HasColumnType("nvarchar(400)");
            builder.Property(_ => _.Email).HasColumnType("nvarchar(300)");

            builder.HasIndex(_ => _.DateAdded);
            builder.HasIndex(_ => _.EntryDate);
            builder.HasIndex(_ => _.DocumentId)
                .IsUnique();

            builder.HasData(new List<Profesor>()
            {
                new Profesor()
                {
                    Id = 1,
                    DateAdded = new DateTime(2025,8,24),
                    DocumentId = "77882323",
                    Names = "Maria",
                    Surnames = "Chavez",
                    EntryDate = DateTime.Now.Date,
                    Email = "example@domain.co"
                },
                new Profesor()
                {
                    Id = 2,
                    DateAdded = new DateTime(2025,8,24),
                    DocumentId = "1237676",
                    Names = "Henry Andres",
                    Surnames = "Hernandez Hernandez",
                    EntryDate = DateTime.Now.Date,
                    Email = "example@domain.co"
                }
            });
        }
    }
}
