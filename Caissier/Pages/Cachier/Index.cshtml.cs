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

        public IndexModel(DBContext context)
        {
            _context = context;
        }

        public IList<Facture> Facture { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Factures != null)
            {
                Facture = await _context.Factures
                .Include(f => f.commande).ToListAsync();
            }
        }
    }
}
