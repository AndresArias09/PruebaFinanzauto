using Domain.Dto.Estudiantes;
using Domain.Entities;
using System.Net.Http.Json;
using Web.Helpers;

namespace Web.Services.Implementations
{
    public class EstudianteService(GetHttpClient getHttpClient)
    {
        private const string _route = "api/Estudiante";

        public async Task<Result<bool?>> CrearNuevoEstudiante(CrearEstudianteRequest request)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{_route}", request);

            Result<bool?> response = result.StatusCode switch
            {
                System.Net.HttpStatusCode.Created => Result<bool?>.Success(true),
                System.Net.HttpStatusCode.BadRequest => await result.Content.ReadFromJsonAsync<Result<bool?>>(),
                System.Net.HttpStatusCode.Unauthorized => Result<bool?>.Failure("Credenciales incorrectas"),
                System.Net.HttpStatusCode.InternalServerError => Result<bool?>.Failure("Ocurrió un error en el sistema"),
                _ => Result<bool?>.Failure("Ocurrió un error desconocido")
            };

            return response;
        }

        public async Task<Result<bool?>> ActualizarEstudiante(EstudianteDto request)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.PutAsJsonAsync($"{_route}", request);

            Result<bool?> response = result.StatusCode switch
            {
                System.Net.HttpStatusCode.NoContent => Result<bool?>.Success(true),
                System.Net.HttpStatusCode.BadRequest => Result<bool?>.Failure(await result.Content.ReadAsStringAsync()),
                System.Net.HttpStatusCode.Unauthorized => Result<bool?>.Failure("Credenciales incorrectas"),
                System.Net.HttpStatusCode.InternalServerError => Result<bool?>.Failure("Ocurrió un error en el sistema"),
                _ => Result<bool?>.Failure("Ocurrió un error desconocido")
            };

            return response;
        }

        public async Task<Result<bool?>> EliminarEstudiante(long id)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.DeleteAsync($"{_route}/{id}");

            Result<bool?> response = result.StatusCode switch
            {
                System.Net.HttpStatusCode.NoContent => Result<bool?>.Success(true),
                System.Net.HttpStatusCode.NotFound => Result<bool?>.Failure("No se encontró el registro"),
                System.Net.HttpStatusCode.BadRequest => Result<bool?>.Failure(await result.Content.ReadAsStringAsync()),
                System.Net.HttpStatusCode.Unauthorized => Result<bool?>.Failure("Credenciales incorrectas"),
                System.Net.HttpStatusCode.InternalServerError => Result<bool?>.Failure("Ocurrió un error en el sistema"),
                _ => Result<bool?>.Failure("Ocurrió un error desconocido")
            };

            return response;
        }

        public async Task<Result<IEnumerable<EstudianteDto>>> GetEstudiantes()
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetAsync($"{_route}/GetAll");

            Result<IEnumerable<EstudianteDto>> response = result.StatusCode switch
            {
                System.Net.HttpStatusCode.OK => Result<IEnumerable<EstudianteDto>>.Success(await result.Content.ReadFromJsonAsync<IEnumerable<EstudianteDto>>()),
                System.Net.HttpStatusCode.BadRequest => Result<IEnumerable<EstudianteDto>>.Failure(await result.Content.ReadAsStringAsync()),
                System.Net.HttpStatusCode.Unauthorized => Result<IEnumerable<EstudianteDto>>.Failure("Credenciales incorrectas"),
                System.Net.HttpStatusCode.InternalServerError => Result<IEnumerable<EstudianteDto>>.Failure("Ocurrió un error en el sistema"),
                _ => Result<IEnumerable<EstudianteDto>>.Failure("Ocurrió un error desconocido")
            };

            return response;
        }


        public async Task<Result<EstudianteDto>> GetEstudianteById(long id)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetAsync($"{_route}/{id}");

            Result<EstudianteDto> response = result.StatusCode switch
            {
                System.Net.HttpStatusCode.OK => Result<EstudianteDto>.Success(await result.Content.ReadFromJsonAsync<EstudianteDto>()),
                System.Net.HttpStatusCode.NotFound => Result<EstudianteDto>.Failure("No se encontró el registro"),
                System.Net.HttpStatusCode.BadRequest => Result<EstudianteDto>.Failure(await result.Content.ReadAsStringAsync()),
                System.Net.HttpStatusCode.Unauthorized => Result<EstudianteDto>.Failure("Credenciales incorrectas"),
                System.Net.HttpStatusCode.InternalServerError => Result<EstudianteDto>.Failure("Ocurrió un error en el sistema"),
                _ => Result<EstudianteDto>.Failure("Ocurrió un error desconocido")
            };

            return response;
        }
    }
}
