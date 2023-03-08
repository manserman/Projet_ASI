using Caissier.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Caissier.Pages.Tables
{
    public class CreateModel : PageModel
    {
        private readonly DBContext _context;

        [BindProperty]
        public Article Article { get; set; }
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

            _context.Articles.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
