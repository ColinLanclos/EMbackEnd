using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backEnd_EM.Interfaces;
using backEnd_EM.Properties.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace backEnd_EM.Service
{

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:SigningKey").Value!));
        }
        public string CreateToken(Athletes athlete)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Email, athlete.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, athlete.Email)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDiscriptor);

            return handler.WriteToken(token);

        }
    }

}