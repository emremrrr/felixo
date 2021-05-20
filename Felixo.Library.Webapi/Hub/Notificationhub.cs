using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Felixo.Library.Webapi
{
    public class Notificationhub : Hub
    {
        public static Dictionary<string, string> ConnectionIdUserInfo = new Dictionary<string, string>();
        public void GetDataFromClient(string userId, string connectionId)
        {
            Clients.Client(connectionId).SendAsync("clientMethodName", $"Updated userid {userId}");
            if (ConnectionIdUserInfo.ContainsKey(userId))
                ConnectionIdUserInfo.Remove(userId);
            ConnectionIdUserInfo.Add(userId, connectionId);
        }
        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            Clients.Client(connectionId).SendAsync("ClientIdMethod", connectionId);
            return base.OnConnectedAsync();
        }
    }
}
