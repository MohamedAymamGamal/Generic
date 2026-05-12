using Ecom.Core.Entities.Identity;
using Ecom.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecom.infrastructure.Reposities.Service
{
    public class GenrateToken : IGenrateToken
    {
        private readonly IConfiguration _configuration;
        public GenrateToken(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task<string> GetAndCreateToken(AppUser user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim (ClaimTypes.Email,user.Email),
                new Claim (ClaimTypes.Name,user.UserName)
            }; 
            var Security = _configuration["Token:Secret"];
            var key = Encoding.ASCII.GetBytes(Security);

            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                //Subject = new System.Security.Claims.ClaimsIdentity(new[]
                //{
                //    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier,user.Id),
                //    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name,user.UserName)
                //}),
                //Expires = DateTime.UtcNow.AddDays(7),
                //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSecurity:SecretKey"])), SecurityAlgorithms.HmacSha256Signature)

                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _configuration["Token:Issuer"],
                SigningCredentials = credentials,
                NotBefore = DateTime.Now
            };  
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
    }
}
