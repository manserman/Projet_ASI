using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetASI.Data;
using ProjetASI.Models;

namespace ProjetASI.Pages.Cachier
{
    public class EncaisserModel : PageModel
    {
        [BindProperty]
        public Commande commande { get; set; }
        [BindProperty]
        public int sommedejapayee { get; set; } = 0;
        private readonly DBContext _context;
        public void OnGet(int id)
        {
            commande = _context.Commandes.FirstOrDefault(cde => cde.ID == id);
        }
    }
}
