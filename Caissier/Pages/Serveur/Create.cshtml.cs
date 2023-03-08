using ProjetASI.Data;
using ProjetASI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace ProjetASI.Pages.Serveur
{
    public class CreateModel : PageModel
    {
        private readonly DBContext _context;

        [BindProperty]
        public ProjetASI.Models.Serveur serveur{ get; set; }
        public CreateModel(DBContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Serveur.Add(serveur);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
