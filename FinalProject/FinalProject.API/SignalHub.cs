using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace FinalProject.API
{
    public class SignalHub: Hub
    {
        private static readonly ConcurrentDictionary<string, User> Users
       = new ConcurrentDictionary<string, User>();
        

        public override Task OnConnectedAsync()
        {

            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName, _ => new User
            {
                Name = userName,
                ConnectionIds = new HashSet<string>()
            });

            lock (user.ConnectionIds)
            {

                user.ConnectionIds.Add(connectionId);

                // TODO: Broadcast the connected user
            }

            return base.OnConnectedAsync();
        }
    }

    public class User
    {

        public string Name { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }
}
