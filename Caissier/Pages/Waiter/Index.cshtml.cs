using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetASI.Data;

namespace ProjetASI.Pages.Waiter
{
    public class IndexModel : PageModel
    {
        private readonly DBContext _context;


        public IndexModel(DBContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }
    }
}
