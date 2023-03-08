using Caissier.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Caissier.Pages.Tables
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Article Articles { get; set; }
        private readonly DBContext _context;

        [BindProperty]
        public Article Article { get; set; }
        public EditModel(DBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            Article = await _context.Articles.FirstOrDefaultAsync(m => m.ID == id);
            return Page();

        }
    }
}
