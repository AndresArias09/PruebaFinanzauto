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

        public AuthController(ILogger<AuthController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        private bool ValidarUsuario(LoginModel model)
        {
            //Aquí debe ir el mecanismo de autenticación real, este es solo demostrativo y no debe usarse así para ambientes reales
            if (model.Username.Equals("Admin") && model.Password.Equals("123"))
            {
                return true;
            }

            return false;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                if (!ValidarUsuario(model))
                {
                    return Unauthorized("Credenciales no válidas");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration.GetSection("ApiAuth:Token").Value!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, model.Username),
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
            catch (Exception exe)
            {
                _logger.LogError(exe, "Error al realizar login");
            }

            return Problem(detail: "Ha ocurrido un error al validar el login");
        }
    }

    public class LoginModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

}
