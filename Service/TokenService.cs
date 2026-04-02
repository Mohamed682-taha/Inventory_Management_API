using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class TokenService(IConfiguration _configuration,UserManager<AppUser> _userManager) : ITokenService
    {
        public async Task<string> CreateToken(AppUser user)
        {
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            List<Claim> Claims =
            [
              new Claim(ClaimTypes.Email , user.Email!),
              new Claim(ClaimTypes.GivenName,user.UserName!),
            ];
            var roles = await _userManager.GetRolesAsync(user);
            Claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role,role)));

            var signingCredentials = new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: Claims,
                    expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationToExpire"]!)),
                    signingCredentials: signingCredentials
                    );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
