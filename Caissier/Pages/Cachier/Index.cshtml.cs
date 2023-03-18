using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Data;
using ProjetASI.Models;

namespace ProjetASI.Pages.Cachier
{
    public class IndexModel : PageModel
    {
        private readonly DBContext _context;
        [BindProperty]
        public IList<Commande> commandes { get; set; }
        public IndexModel(DBContext context)
        {
            _context = context;
        }

        

        public async Task<IActionResult> OnGetAsync()
        {
         
                commandes = await _context.Commandes.
                    Include(ce => ce.serveur).
                    Include(ce => ce.table)
                    .Include(ce => ce.Articles)
                .ThenInclude(art => art.article).Where(cde => cde.isServed== true && (cde.isPaid==false || cde.isPaid==null)).ToListAsync();
            return Page();
        }
    }
}
