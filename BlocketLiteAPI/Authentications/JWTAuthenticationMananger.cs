using BlocketLiteEFCoreDB.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlocketLiteAPI.Authentications
{
    /// <summary>
    /// Class that contains the JWTAuthenticationManager, creats ad encrypt a JWT token when called
    /// </summary>
    public class JWTAuthenticationMananger : IJWTAuthenticationMananger
    {
        private readonly string _key;
        public JWTAuthenticationMananger(string key)
        {
            _key = key;
        }
        public string Authentication(User user, string password)
        {
            if (user.Password != password)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(200),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
