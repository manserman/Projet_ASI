using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Data;
using ProjetASI.Hubs;
using ProjetASI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ProjetASI.Pages.Waiter
{
    [Authorize(Roles = "Serveur")]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Serveur user { get; set; }
        private readonly DBContext _context;
        private readonly IHubContext<CommandeHub> _hubContext;
        [BindProperty]
        public IList<Commande> commandes { get; set; }
        [BindProperty]
        public IList<Table> tables { get; set; }
        private readonly UserManager<IdentityUser> _userManager;
        public IndexModel(DBContext context, IHubContext<CommandeHub> hubcontext, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hubContext = hubcontext;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {



            // Vérifier que l'utilisateur est authentifié
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                // Obtenir l'ID de l'utilisateur actuel
                IdentityUser current = await _userManager.GetUserAsync(HttpContext.User);
                user = _context.Serveur.FirstOrDefault(usr => usr.UserID == current.Id);


                commandes = _context.Commandes.Where(cde => cde.serveurId == user.ID && cde.isServed == false).ToList();
                tables = _context.Tables.ToList();
            }
            return Page();

        }

        public async Task<IActionResult> OnPostPremierAsync(int id)
        {
             Table table = await _context.Tables.FirstOrDefaultAsync(me => me.ID == id);
            table.occuppe =false;

            _context.Attach(table).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return RedirectToPage("./Index");

        }

        public async Task<IActionResult> OnPostServieAsync(int id)
        {   Commande cmde= await _context.Commandes.FirstOrDefaultAsync(cde => cde.ID==id);
            cmde.isServed = true;
            _context.Attach(cmde).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("CommandeServie");
            return RedirectToPage("./Index");
        }
    }

}
