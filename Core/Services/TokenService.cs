using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FleetCommandAPI.Core.Entity.User;
using Microsoft.IdentityModel.Tokens;

namespace FleetCommandAPI.Core.Services
{
    public class TokenService
    {

        public string GenerateToken(UserModel user)
        {
            Claim[] claims = new Claim[]{
            new Claim("userName", user.UserName),
            new Claim("ID", user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("pulacavaloeboipulacowboypulacavaloeboipulacowboy"));
            var signinCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(30),
                claims: claims,
                signingCredentials: signinCredential
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}