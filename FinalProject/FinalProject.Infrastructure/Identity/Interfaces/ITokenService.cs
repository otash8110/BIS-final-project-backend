
namespace FinalProject.Infrastructure.Identity.Interfaces
{
    public interface ITokenService
    {
        string CreateUserToken(ApplicationUser user, IList<string> userRoles);
    }
}
