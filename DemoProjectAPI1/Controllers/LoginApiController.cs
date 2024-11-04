using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IRepositroy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DemoProjectAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {
        private readonly IloginInterface _loginservice;
        private readonly IConfiguration _configuration;

        public LoginApiController(IloginInterface loginservice, IConfiguration configuration)
        {
            _loginservice = loginservice;
            _configuration = configuration;
        }

        [HttpGet("login")]
        public IActionResult Login(string username, string password)
        {
            // Check if the username and password are provided
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Username and password are required.");
            }

            // Call the login service with username and password
            var user = _loginservice.Login(username, password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("UserId", user.id.ToString()),
                    // Add additional claims as necessary
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Return token in response
            return Ok(new
            {
                Token = tokenString,
                User = user // Optionally include user details
            });
        }
    }
}
