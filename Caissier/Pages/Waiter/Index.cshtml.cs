using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Data;
using ProjetASI.Hubs;
using ProjetASI.Models;

namespace ProjetASI.Pages.Waiter
{
    public class IndexModel : PageModel
    {
        private readonly DBContext _context;
        private readonly IHubContext<CommandeHub> _hubContext;
        [BindProperty]
        public IList<Commande> commandes { get; set; }
        [BindProperty]
        public IList<Table> tables { get; set; }
        
        public IndexModel(DBContext context, IHubContext<CommandeHub> hubcontext)
        {
            _context = context;
            _hubContext = hubcontext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            commandes = await _context.Commandes.Where(cde => cde.serveurId == 1 && cde.isServed==false).ToListAsync();
            tables = _context.Tables.ToList();
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
