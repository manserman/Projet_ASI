using ProjetASI.Data;
using ProjetASI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ProjetASI.Pages.Tables
{
    [Authorize(Roles = "Admin")]
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

        public async Task<IActionResult> OnPostCreationAsync(int id)
        {
            Table = new Table();
            Table.occuppe = false;

            _context.Tables.Add(Table);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null || _context.Tables == null)
            {
                return NotFound();
            }
            var table = await _context.Tables.FindAsync(id);

            if (table != null)
            {
                Table = table;
                _context.Tables.Remove(Table);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
