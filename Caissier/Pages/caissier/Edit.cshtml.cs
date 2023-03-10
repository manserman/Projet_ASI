using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Data;
using ProjetASI.Models;

namespace ProjetASI.Pages.caissier
{
    public class EditModel : PageModel
    {
        private readonly ProjetASI.Data.DBContext _context;

        public EditModel(ProjetASI.Data.DBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Caissier Caissier { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Caissiers == null)
            {
                return NotFound();
            }

            var caissier =  await _context.Caissiers.FirstOrDefaultAsync(m => m.ID == id);
            if (caissier == null)
            {
                return NotFound();
            }
            Caissier = caissier;
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

            _context.Attach(Caissier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaissierExists(Caissier.ID))
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

        private bool CaissierExists(int id)
        {
          return _context.Caissiers.Any(e => e.ID == id);
        }
    }
}
