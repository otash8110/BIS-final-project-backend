using FinalProject.Infrastructure.Identity.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalProject.Infrastructure.Identity
{
    public class TokenService: ITokenService
    {
        private readonly ITokenSettings tokenSettings;

        public TokenService(IOptions<ITokenSettings> tokenSettings)
        {
            this.tokenSettings = tokenSettings.Value;
        }

        public string CreateUserToken(ApplicationUser user, IList<string> userRoles)
        {
            List<Claim> claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim("UserId", user.UserId.ToString()),
                        };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                tokenSettings.JwtKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
