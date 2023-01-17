using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FinalProject.API
{
    public class SignalHub: Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        [Authorize]
        public async Task SendMessage(string msg)
        => await Clients.All.SendAsync("send", msg);
    }

    public class User
    {

        public string Name { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }
}
