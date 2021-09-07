using Auth.API.Configuration;
using Auth.API.Entities;
using Auth.API.Repositories;
using Auth.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly AuthSettings _authSettings;
        public AuthController(IAuthRepository authRepository, IOptions<AuthSettings> authSettings)
        {
            _authRepository = authRepository;
            _authSettings = authSettings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel login)
        {
            try
            {
                var passwordHash = GetPasswordHash(login.Password);

                var user = await _authRepository.Authenticate(login.Email, passwordHash);

                if (user != null)
                    return Ok(GetJwt(user));

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private static string GetPasswordHash(string password)
        {
            var value = Encoding.UTF8.GetBytes(password);
            var sha256 = SHA256.Create();
            var hashValue = sha256.ComputeHash(value);
            var builder = new StringBuilder();

            foreach (var valor in hashValue)
            {
                builder.Append(valor.ToString("X2"));
            }

            return builder.ToString();
        }

        private string GetJwt(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _authSettings.ValidAudiences,
                Expires = DateTime.UtcNow.AddMinutes(_authSettings.ExpiresMinute),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var response = new Dictionary<string, object>();
            response.Add("token", tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)));
            response.Add("userName", user.Name);
            response.Add("userId", user.Id);
            response.Add("userEmail", user.Email);
            response.Add("expirationToken", DateTime.UtcNow.AddMinutes(_authSettings.ExpiresMinute));

            var json = JsonConvert.SerializeObject(response);

            return json;
        }
    }
}
