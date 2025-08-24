using EjemploApi.API.Services;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EjemploApi.API
{
    public static class WebApplicationExtensionMethods
    {
        public static WebApplication SetCors(this WebApplication app)
        {
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            return app;
        }

        public static WebApplication ConfigureLogging(this WebApplication app)
        {
            app.UseMiddleware<LogUserNameMiddleware>();
            app.UseSerilogRequestLogging(options =>
            {
                // Customize the message template
                options.MessageTemplate = "{RemoteIpAddress} {RequestScheme} {RequestHost} {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

                // Emit debug-level events instead of the defaults

                // Attach additional properties to the request completion event
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                    diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress);
                };
            });

            return app;
        }

        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            Log.Information("Migración de base de datos INICIADA");

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                dbContext.Database.Migrate();
            }

            Log.Information("Migración de base de datos FINALIZADA");

            return app;
        }
    }
}
