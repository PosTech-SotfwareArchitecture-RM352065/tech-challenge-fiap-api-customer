using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sanduba.Auth.Api.Gateway
{
    public class SecurityGateway 
    {
        public string GenerateJwt(Guid? userId = null)
        {
            var claims = new Claim[] { new Claim("Sub", userId.ToString() )
            };

            var jwtSecretKey = Environment.GetEnvironmentVariable("AUTH_SECRET_KEY");
            var jwtIssuer = Environment.GetEnvironmentVariable("AUTH_ISSUER");
            var jwtAudience = Environment.GetEnvironmentVariable("AUTH_AUDIENCE");

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                jwtIssuer,
                jwtAudience,
                claims,
                null,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken( token );
        }

        public string GetHashPassword(string password)
        {
            return password;
        }
    }
}
