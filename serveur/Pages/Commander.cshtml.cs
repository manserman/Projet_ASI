using DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace serveur.Pages
{
    public class CommanderModel : PageModel
    {
        public IList<Table> Tables { get; set; }
        [BindProperty]
        public IList<Article> Articles { get; set; }

        private readonly DBContext _context;

     
        public CommanderModel(DBContext context)
        {
            _context = context;
        }



        public async Task OnGetAsynch()
        {

            Articles = await _context.Articles.ToListAsync();
            Tables = _context.Tables.ToList();
        }
    }
}
