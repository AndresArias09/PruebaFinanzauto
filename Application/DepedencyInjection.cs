using Application.Services;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEstudianteService, EstudianteService>();
            services.AddScoped<IProfesorService, ProfesorService>();
            services.AddScoped<ICursoService, CursoService>();

            return services;
        }
    }
}
