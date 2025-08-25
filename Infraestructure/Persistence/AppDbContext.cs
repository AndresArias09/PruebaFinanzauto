using Domain.Entities;
using Infraestructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<CursoEstudiante> CursoEstudiante { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfig());
            modelBuilder.ApplyConfiguration(new EstudianteConfig());
            modelBuilder.ApplyConfiguration(new ProfesorConfig());
            modelBuilder.ApplyConfiguration(new CursoConfig());
            modelBuilder.ApplyConfiguration(new CursoEstudianteConfig());
            modelBuilder.ApplyConfiguration(new CalificacionConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
