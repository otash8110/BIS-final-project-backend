using Microsoft.AspNetCore.Authorization;

namespace FinalProject.API.Policies
{
    public class IsRegisteredRequirement: IAuthorizationRequirement
    {
        public string isRegistered { get; }

        public IsRegisteredRequirement(string isRegistered)
        {
            this.isRegistered = isRegistered;
        }
    }
}
