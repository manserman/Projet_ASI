using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ProjetASI.Models;

namespace ProjetASI.Hubs
{ 
    public class CommandeHub : Hub
    {
        
        
        public async Task RecevoirCommande()
            {
          
                await Clients.All.SendAsync("RecevoirCommande");
            }
        public async Task CommandePrete(int id)
        {

            await Clients.All.SendAsync("CommandePrete", id);
        }
        public async Task CommandeServie()
        {

            await Clients.All.SendAsync("CommandeServie");
        }
    }
}
