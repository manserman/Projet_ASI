using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using ProjetASI.Data;
using ProjetASI.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetASI.Pages.caissier
{
    public class CreateModel : PageModel
    {
        private readonly DBContext _context;

        [BindProperty]
        public ProjetASI.Models.Caissier caissier { get; set; }
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
            if (caissier != null)
            {
                _context.Caissiers.Add(caissier);
            }
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            
        }
    }
}
