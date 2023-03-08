using Caissier.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Caissier.Pages.caissier
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Caissier.Data.Caissier> caissiers { get; set; }
        public Caissier.Data.Caissier Caissier { get; set; }
        private readonly DBContext _context;

        public IndexModel(DBContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            caissiers = await _context.Caissiers.ToListAsync();


        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(caissiers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(Caissier.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
        private bool ArticleExists(int id)
        {
            return _context.Caissiers.Any(e => e.ID == id);
        }
    }

}
