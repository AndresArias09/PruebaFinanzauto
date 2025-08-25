using Domain.Dto;
using Domain.Entities;
using System.Net.Http.Json;
using Web.Helpers;
using Web.Services.Contracts;


namespace Web.Services.Implementations
{
    public class UserAccountService(GetHttpClient getHttpClient) : IUserAccountService
    {
        public const string AuthUrl = "api/auth";

        public async Task<Result<LoginResponse>> IniciarSesion(LoginRequest request)
        {
            var httpClient = getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/login", request);

            Result<LoginResponse> response = result.StatusCode switch
            {
                System.Net.HttpStatusCode.OK => Result<LoginResponse>.Success(await result.Content.ReadFromJsonAsync<LoginResponse>()),
                System.Net.HttpStatusCode.Unauthorized => Result<LoginResponse>.Failure("Credenciales incorrectas"),
                System.Net.HttpStatusCode.InternalServerError => Result<LoginResponse>.Failure("Ocurrió un error en el sistema"),
                _ => Result<LoginResponse>.Failure("Ocurrió un error desconocido")
            };

            return response;
        }

    }
}
