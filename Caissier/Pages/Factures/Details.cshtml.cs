using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Data;
using ProjetASI.Models;

namespace ProjetASI.Pages.Factures
{
    public class DetailsModel : PageModel
    {
        private readonly ProjetASI.Data.DBContext _context;

        public DetailsModel(ProjetASI.Data.DBContext context)
        {
            _context = context;
        }

      public Facture Facture { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Factures == null)
            {
                return NotFound();
            }

            var facture = await _context.Factures.FirstOrDefaultAsync(m => m.ID == id);
            if (facture == null)
            {
                return NotFound();
            }
            else 
            {
                Facture = facture;
            }
            return Page();
        }
    }
}
