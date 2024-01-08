using Microsoft.AspNetCore.Identity;
using MovieStore.Data.Entities;
using MovieStore.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<AppUser> _userManager;

        public TokenService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> CreateToken(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return ("Username or password incorrect.");
            }
            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                return ("Username or password incorrect.");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(600);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("securitykeysecuritykeysecuritykey"));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, "www.myapi.com"));

            userRoles.ToList().ForEach(x =>
            {
                claims.Add(new Claim(ClaimTypes.Role, x));
            });

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: "www.myapi.com",
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                claims: claims,
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
