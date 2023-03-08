using ProjetASI.Data;
using ProjetASI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjetASI.Pages.caissier
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public ProjetASI.Models.Caissier caissier { get; set; }
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
