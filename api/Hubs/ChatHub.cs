using Microsoft.AspNetCore.SignalR;
using MySqlX.XDevAPI;
using System.Collections.Concurrent;

namespace Api.Hubs {
  public class ChatHub : Hub {

    private static readonly ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();


    public override Task OnConnectedAsync() {
      var username = Context.GetHttpContext().Request.Query["username"].ToString();
      if (string.IsNullOrEmpty(username)) {
        username = "anonymous";
      }

      _connections[username] = Context.ConnectionId;
      return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception) {
      var username = Context.User.Identity.Name ?? "anonymous";
      _connections.TryRemove(username, out _);
      return base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessageToUser(string receiverUsername, string message) {
      if (_connections.TryGetValue(receiverUsername, out var receiverConnectionId)) {
        await Clients.Client(receiverConnectionId).SendAsync("ReceivePrivateMessage", message);
      }
    }


    public async Task SendMessage(string user, string message) {
      await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
  }
}
