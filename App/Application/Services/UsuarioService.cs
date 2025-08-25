using Domain.Dto;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Result<bool>> ValidarLogin(LoginRequest login)
        {
            var usuario = await _usuarioRepository.GetUsuarioByUserName(login.User);

            if (usuario is null) throw new UserCredentialsNotValidException("Credenciales no válidas");

            if (!PasswordHasher.VerifyPassword(login.Password, usuario.Password))
            {
                throw new UserCredentialsNotValidException("Credenciales no válidas");
            }

            return Result<bool>.Success(true);
        }
    }
}
