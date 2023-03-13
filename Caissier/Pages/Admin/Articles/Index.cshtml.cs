using ProjetASI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetASI.Models;


using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;

namespace ProjetASI.Pages.Articles
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Article> Articles { get; set; }
        private readonly DBContext _context;

        [BindProperty]
        public Article Article { get; set; }
        public IndexModel(DBContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            Articles = await _context.Articles.ToListAsync();


        }
    }
}
