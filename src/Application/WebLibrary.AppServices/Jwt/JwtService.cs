using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.Contracts.Users;

namespace WebLibrary.AppServices.Jwt
{
    ///<inheritdoc cref="IJwtService"/>
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Создания экземпляра класса <see cref="JwtService"/>
        /// </summary>
        /// <param name="configuration">Конфигурация.</param>
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        ///<inheritdoc/>
        public async Task<string> GetToken(LoginRequest userData, Guid id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userData.Login),
                    new Claim(ClaimTypes.NameIdentifier, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
