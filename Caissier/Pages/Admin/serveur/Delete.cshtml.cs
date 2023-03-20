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
using Microsoft.AspNetCore.Identity;

namespace ProjetASI.Pages.serveur
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly DBContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DeleteModel(DBContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
      public Serveur Serveur { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Serveur == null)
            {
                return NotFound();
            }

            var serveur = await _context.Serveur.FindAsync( id);

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
                
                var user = await _userManager.FindByIdAsync(serveur.UserID);
                var defaultRole = _roleManager.FindByNameAsync("Serveur").Result;
                if (user == null)
                {
                    return NotFound();
                }
                serveur.UserID = null;

                _context.Attach(Serveur).State = EntityState.Modified;
                await _context.SaveChangesAsync();
               
                var result = await _userManager.RemoveFromRoleAsync(user, defaultRole.Name);
                await _userManager.DeleteAsync(user);



            }

            return RedirectToPage("./Index");
        }
    }
}
