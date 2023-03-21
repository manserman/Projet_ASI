using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Data;
using ProjetASI.Models;

namespace ProjetASI.Pages.serveur
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ProjetASI.Data.DBContext _context;

        public EditModel(ProjetASI.Data.DBContext context)
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

            var serveur =  await _context.Serveur.Where(serv =>serv.UserID!=null).FirstOrDefaultAsync(m => m.ID == id);
            if (serveur == null)
            {
                return NotFound();
            }
            Serveur = serveur;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Serveur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServeurExists(Serveur.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ServeurExists(int id)
        {
          return _context.Serveur.Any(e => e.ID == id);
        }
    }
}
