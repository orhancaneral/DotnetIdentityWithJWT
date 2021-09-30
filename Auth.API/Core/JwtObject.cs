using Auth.API.Core.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.API.Core
{
    public interface IJwtObject
    {
        string Create(string name, List<string> roles);
    }
    public class JwtObject : IJwtObject
    {
        private readonly IConfiguration _configuration;
        public JwtObject(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Create(string name, List<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection(Constants.SecretKey).Value);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, name));
            roles.ForEach(oe => claims.Add(new Claim(ClaimTypes.Role, oe)));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
