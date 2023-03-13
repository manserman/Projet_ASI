using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ProjetASI.Models;

namespace ProjetASI.Hubs
{
    public class CommandeHub : Hub
    {
        public async Task EnvoyerCommande(String nouvelle)
        {
            await Clients.All.SendAsync("RecevoirCommande", nouvelle);
        }
    }
}
