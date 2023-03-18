using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Data;
using ProjetASI.Models;

namespace ProjetASI.Pages.Cachier
{
    public class EncaisserModel : PageModel
    {
        [BindProperty]
        public Commande commande { get; set; }
        [BindProperty]
        public int sommedejapayee { get; set; } = 0;
        
        private readonly DBContext _context;
        public EncaisserModel(DBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            commande =await _context.Commandes.FirstOrDefaultAsync(cde => cde.ID == id);
            return Page();
        }
    }
}
