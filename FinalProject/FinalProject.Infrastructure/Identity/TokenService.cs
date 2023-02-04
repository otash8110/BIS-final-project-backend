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
        private readonly TokenSettings tokenSettings;

        public TokenService(IOptions<TokenSettings> tokenSettings)
        {
            this.tokenSettings = tokenSettings.Value;
        }

        public string CreateUserToken(ApplicationUser user, IList<string> userRoles)
        {
            List<Claim> claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.NameIdentifier, user.UserName),
                            new Claim("UserId", user.Id),
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
