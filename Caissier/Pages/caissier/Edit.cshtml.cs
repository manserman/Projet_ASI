using Caissier.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Caissier.Pages.caissier
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Caissier.Data.Caissier caissier { get; set; }
        private readonly DBContext _context;

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

            caissier = await _context.Caissiers.FirstOrDefaultAsync(m => m.ID == id);
            return Page();

        }
    }
}
