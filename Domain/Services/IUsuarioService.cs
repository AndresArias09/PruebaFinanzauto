using Domain.Dto;
using Domain.Entities;

namespace Domain.Services
{
    public interface IUsuarioService
    {
        public Task<Result<bool>> ValidarLogin(LoginRequest login);
    }
}
