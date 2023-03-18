using ProjetASI.Data;
using ProjetASI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using ProjetASI.Hubs;


namespace ProjetASI.Pages.Barman
{
  
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Commande> commandes { get; set; }
        private readonly DBContext _context;
        private readonly IHubContext<CommandeHub> _hubContext;
        public IndexModel(DBContext context, IHubContext<CommandeHub> hubcontext)
        {
            _context = context;
            _hubContext = hubcontext;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            commandes = await _context.Commandes.Include(c => c.Articles).ThenInclude(art=> art.article).Where(ce => ce.validee == false).ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            Commande cmde = await _context.Commandes.FirstOrDefaultAsync(cd => cd.ID == id);
            if ((bool)!cmde.commencer)
                cmde.commencer = true;
            else
                cmde.validee = true;
            _context.Attach(cmde).State=EntityState.Modified;
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("CommandePrete",1);
            return RedirectToAction("./Index");
        }
    }
}
