using ProjetASI.Data;
using ProjetASI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProjetASI.Pages.Order
{
    public class CommanderModel : PageModel
    {
        [BindProperty]
        public int[] qCommander { get; set; }
        [BindProperty]
        public int quantite { get; set; }
        [BindProperty]
        public IList<Article> Articles { get; set; }
        private readonly DBContext _context;

        [BindProperty]
        public Article Article { get; set; }
        public CommanderModel(DBContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            Articles = await _context.Articles.ToListAsync();


        }
    }
}
