using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Data;
using ProjetASI.Models;

namespace ProjetASI.Pages.serveur
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ProjetASI.Data.DBContext _context;

        public DeleteModel(ProjetASI.Data.DBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Serveur Serveur { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Serveur == null)
            {
                return NotFound();
            }

            var serveur = await _context.Serveur.FirstOrDefaultAsync(m => m.ID == id);

            if (serveur == null)
            {
                return NotFound();
            }
            else 
            {
                Serveur = serveur;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Serveur == null)
            {
                return NotFound();
            }
            var serveur = await _context.Serveur.FindAsync(id);

            if (serveur != null)
            {
                Serveur = serveur;
                _context.Serveur.Remove(Serveur);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
