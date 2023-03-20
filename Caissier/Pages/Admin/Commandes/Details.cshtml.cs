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

namespace ProjetASI.Pages.Admin.Commandes
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly ProjetASI.Data.DBContext _context;

        public DetailsModel(ProjetASI.Data.DBContext context)
        {
            _context = context;
        }

      public Commande Commande { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Commandes == null)
            {
                return NotFound();
            }

             var commande = _context.Commandes
                .Include(c =>c.serveur)
                    .Include(c => c.Articles)
                    .ThenInclude(lgn => lgn.article)
                    .FirstOrDefault(c => c.ID == id);
            if (commande == null)
            {
                return NotFound();
            }
            else 
            {
                Commande = commande;
            }
            return Page();
        }
    }
}
