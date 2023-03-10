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
    public class DetailsModel : PageModel
    {
        private readonly ProjetASI.Data.DBContext _context;

        public DetailsModel(ProjetASI.Data.DBContext context)
        {
            _context = context;
        }

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
    }
}
