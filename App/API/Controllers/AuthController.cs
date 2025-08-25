using Domain.Dto;
using Domain.Exceptions;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EjemploApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;
        public AuthController
        (
            ILogger<AuthController> logger,
            IConfiguration configuration,
            IUsuarioService usuarioService
        )
        {
            _logger = logger;
            _configuration = configuration;
            _usuarioService = usuarioService;
        }

        private async Task<bool> ValidarUsuario(LoginRequest model)
        {
            var resultUser = await _usuarioService.ValidarLogin(model);

            if (resultUser.IsSuccess)
            {
                return true;
            }

            return false;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest model)
        {
            try
            {
                if (!await ValidarUsuario(model))
                {
                    return Unauthorized("Credenciales no válidas");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration.GetSection("ApiAuth:Token").Value!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, model.User),
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _configuration.GetSection("ApiAuth:TokenIssuer").Value!,
                    Audience = _configuration.GetSection("ApiAuth:TokenAudience").Value!,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
            }
            catch (UserCredentialsNotValidException exe)
            {
                return Unauthorized(exe.Message);
            }
            catch (Exception exe)
            {
                _logger.LogError(exe, "Error al realizar login");
            }

            return Problem(detail: "Ha ocurrido un error al validar el login");
        }
    }
}
