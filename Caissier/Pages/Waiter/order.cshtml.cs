using ProjetASI.Data;
using ProjetASI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using ProjetASI.Hubs;

namespace ProjetASI.Pages.Waiter.Order
{
    public class CommanderModel : PageModel
    {
        [BindProperty]
        public string[] quantitesCommandees { get; set; }
        [BindProperty]
        public int quantite { get; set; }
        [BindProperty]
        public IList<Article> Articles { get; set; }
        private readonly DBContext _context;
        private IHubContext<CommandeHub> hubContext;
        [BindProperty]
        public IList<Table> TablesOccupeessanscommandes { get; set; }
        [BindProperty]
        public Article Article { get; set; }
        [BindProperty]
        public int prixt { get;  set; }
        [BindProperty]
        public  Commande commande { get; set; }
        public CommanderModel(DBContext context, IHubContext<CommandeHub> hubcontext)
{
                  _context = context;
                 this.hubContext = hubcontext;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            Articles = await _context.Articles.ToListAsync();
            TablesOccupeessanscommandes = _context.Tables.Where(t => t.occuppe == true && t.commandePrise==false).ToList();

            ViewData["Tables"] = new SelectList(TablesOccupeessanscommandes
            , "ID", "ID");
            
            return Page();
            

        }
        public async Task<IActionResult> OnPostFirstAsync()
        {
            if(quantitesCommandees!=null)
            { 
                
                commande.datecomm = DateTime.Now;
                commande.serveurId = 2;
                commande.validee = false;
                commande.commencer = false;
                Articles = await _context.Articles.ToListAsync();
                _context.Commandes.Add(commande);
                await _context.SaveChangesAsync();
                for (int i=0; i< quantitesCommandees.Length; i++)
                {
                    int qte;
                    int.TryParse(quantitesCommandees[i],out qte);
                    if (qte>0)
                    {
                        LigneCommande A = new LigneCommande();
                        A.ArticleID=Articles[i].ID;
                        A.CommandeID = commande.ID;
                        A.quantite=qte;
                        A.prix = Articles[i].prixU * qte;
                        _context.LigneCommandes.Add(A);
                        Articles[i].quantite-=qte;
                        _context.Attach(Articles[i]).State = EntityState.Modified;
                    }
                    
                }
                Table table= _context.Tables.FirstOrDefault(t => t.ID == commande.tableID);
                table.commandePrise = true;
                _context.Attach(table).State = EntityState.Modified;
                
                 await _context.SaveChangesAsync();
                commande = await _context.Commandes.Include(c => c.Articles).FirstOrDefaultAsync(cde => cde.ID == commande.ID);
                await hubContext.Clients.All.SendAsync("RecevoirCommande", "Nouvelle Commande");
            }
            return RedirectToPage("./order");

        }
    }
}
