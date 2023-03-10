using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Data;
using ProjetASI.Models;

namespace ProjetASI.Pages.caissier
{
    public class DeleteModel : PageModel
    {
        private readonly ProjetASI.Data.DBContext _context;

        public DeleteModel(ProjetASI.Data.DBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Caissier Caissier { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Caissiers == null)
            {
                return NotFound();
            }

            var caissier = await _context.Caissiers.FirstOrDefaultAsync(m => m.ID == id);

            if (caissier == null)
            {
                return NotFound();
            }
            else 
            {
                Caissier = caissier;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Caissiers == null)
            {
                return NotFound();
            }
            var caissier = await _context.Caissiers.FindAsync(id);

            if (caissier != null)
            {
                Caissier = caissier;
                _context.Caissiers.Remove(Caissier);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
