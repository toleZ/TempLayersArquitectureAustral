using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TempLayersArquitectureAustral.Models;

namespace TempLayersArquitectureAustral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] Credentials credentials)
        {
            // Authenticate the user
            User? userToAuthenticate = _userService.AuthenticateUser(credentials.Username, credentials.Password);
            if (userToAuthenticate == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            // Generate the JWT token
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userToAuthenticate.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, userToAuthenticate.Name),
            };

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Authentication:MinutesToExpire"]!)),
                signingCredentials: signingCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return Ok(new { Token = tokenString });
        }
    }
}
