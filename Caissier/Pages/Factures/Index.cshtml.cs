using Caissier.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Caissier.Pages.Factures
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Facture> Factures{ get; set; }
        private readonly DBContext _context;

        [BindProperty]
        public Article Article { get; set; }
        public IndexModel(DBContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            Factures = await _context.Factures.ToListAsync();
            


        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(Article.ID))
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
            return _context.Articles.Any(e => e.ID == id);
        }
    }

}
