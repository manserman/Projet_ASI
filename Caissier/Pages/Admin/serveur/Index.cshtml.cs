using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Data;
using ProjetASI.Models;

namespace ProjetASI.Pages.serveur
{
    public class IndexModel : PageModel
    {
        private readonly ProjetASI.Data.DBContext _context;

        public IndexModel(ProjetASI.Data.DBContext context)
        {
            _context = context;
        }

        public IList<Serveur> Serveur { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Serveur != null)
            {
                Serveur = await _context.Serveur.ToListAsync();
            }
        }
    }
}
