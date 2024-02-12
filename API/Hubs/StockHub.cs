using API.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace API.Hubs
{
    public class StockHub : Hub
    {
        public async Task JoinGroup(string groupName)
        
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
