using Domain.Repositories;
using Infraestructure.Persistence;
using Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), o =>
                    {
                        o.EnableRetryOnFailure(2);
                        o.CommandTimeout(30);

                    });
                options.EnableDetailedErrors(true);
                options.EnableSensitiveDataLogging(true);
            });

            //Repositories
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEstudianteRepository, EstudianteRepository>();
            services.AddScoped<IProfesorRepository, ProfesorRepository>();
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<ICursoEstudianteRepository, CursoEstudianteRepository>();
            services.AddScoped<ICalificacionRepository, CalificacionRepository>();

            return services;
        }
    }
}
