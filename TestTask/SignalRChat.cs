using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.SignalR;
using System;

namespace TestTask
{
    public class SignalRChat : Hub
    {
        public override Task OnConnectedAsync()
        {
            Clients.All.SendAsync("hello" + Clients.Caller.ToString());
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

    }
}