using Domain.Dto;
using Domain.Entities;
using Web.Helpers;

namespace Web.Services.Contracts
{
    public interface IUserAccountService
    {
        public Task<Result<LoginResponse>> IniciarSesion(LoginRequest request);
    }
}
