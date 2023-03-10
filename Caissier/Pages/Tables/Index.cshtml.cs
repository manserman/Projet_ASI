using ProjetASI.Data;
using ProjetASI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ProjetASI.Pages.Tables
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Table> Tables { get; set; }
        private readonly DBContext _context;

        [BindProperty]
        public Table Table { get; set; }
        public IndexModel(DBContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            Tables = await _context.Tables.ToListAsync();


        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Tables).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(Table.ID))
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
            return _context.Tables.Any(e => e.ID == id);
        }
    }

}
