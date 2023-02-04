using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace FinalProject.API
{
    [Authorize]
    public class SignalHub: Hub
    {
        private string userGroupName;
        private string adminGroupName;

        public SignalHub(IConfiguration configuration)
        {
            userGroupName = configuration["UsersGroup"];
            adminGroupName = configuration["AdminsGroup"];
        }

        public override async Task<Task> OnConnectedAsync()
        {
            var identity = (ClaimsIdentity)Context.User.Identity;
            var role = identity.FindFirst(ClaimTypes.Role).Value;
            var email = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

            switch (role)
            {
                case "User":
                    await AddToUsersGroup(Context.ConnectionId);
                    break;
                case "Admin":
                    await AddToAdminsGroup(Context.ConnectionId);
                    break;
            }

            return base.OnConnectedAsync();
        }

        public async Task SendMessage(string msg)
        => await Clients.All.SendAsync("send", msg);

        private async Task AddToUsersGroup(string connectionId)
        {
            await Groups.AddToGroupAsync(connectionId, userGroupName);
        }

        private async Task AddToAdminsGroup(string connectionId)
        {
            await Groups.AddToGroupAsync(connectionId, adminGroupName);
        }
    }
}
