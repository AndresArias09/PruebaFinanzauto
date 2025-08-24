using EjemploApi.API;
using EjemploAPI.Api;
using Infraestructure;
using Serilog;

//Logs
var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

Log.Information("Starting up the application");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.ConfigureLogging(logging =>
    {
        logging.ClearProviders();
    });
    //Serilog
    builder.Host.UseSerilog();

    #region Capas
    //Infraestructure
    builder.Services.AddInfraestructure(builder.Configuration);

    //Api
    builder.Services.AddApi(builder.Configuration);
    #endregion

    var app = builder.Build();

    app.MigrateDatabase();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.SetCors();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.ConfigureLogging();

    app.Run();
}
catch (Exception exe)
{
    Log.Fatal(exe, "There was a problem starting the application");
    return;
}
finally
{
    Log.CloseAndFlush();
}
