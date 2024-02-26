using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiContenedores.Datos;
using WebApiContenedores.Models;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebApiContenedores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly string secretKey;

        public AutenticacionController(IConfiguration config)
        {
            secretKey = config.GetSection("Settings").GetSection("secretKey").ToString();
        }

        [HttpPost("Validar")]
        public IActionResult validar([FromBody] UsuarioModel model)
        {
            if (model.vc_correo == "joseph@gmail.com" && model.vc_clave == "123456")
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, model.vc_correo));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(25),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new { vc_token = tokenCreado});
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new {token = ""});
            }

        }

    }
}
