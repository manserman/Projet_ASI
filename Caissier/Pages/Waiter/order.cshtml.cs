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
    using System.Security.Claims;

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
            private readonly IHubContext<CommandeHub> _hubContext;
        [BindProperty]
            public IList<Table> TablesOccupeessanscommandes { get; set; }
            [BindProperty]
            public Article Article { get; set; }
            [BindProperty]
            public int prixt { get;  set; }
        public Serveur serv { get; set; }
        private IList<Serveur> serveurs;
            [BindProperty]
            public  Commande commande { get; set; }
            public CommanderModel(DBContext context, IHubContext<CommandeHub> hubcontext)
    {
                      _context = context;
            _hubContext= hubcontext;
            }
            public async Task<IActionResult> OnGetAsync()
            {  
                Articles = await _context.Articles.ToListAsync();
                TablesOccupeessanscommandes = _context.Tables.Where(t => t.occuppe == false).ToList();

                ViewData["Tables"] = new SelectList(TablesOccupeessanscommandes
                , "ID", "ID");
            
                return Page();
            

            }
            public async Task<IActionResult> OnPostFirstAsync()
            {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            serveurs = await _context.Serveur.Where(se => se.UserID == userId).ToListAsync();
            serv = serveurs.FirstOrDefault();
            if (quantitesCommandees!=null)
                { 
                
                    commande.datecomm = DateTime.Now;
                    commande.serveurId = serv.ID;
                    commande.validee = false;
                    commande.commencer = false;
                    commande.isServed = false;
                    Articles = await _context.Articles.ToListAsync();
                    _context.Commandes.Add(commande);
                    await _context.SaveChangesAsync();
                int id= commande.ID;
                Console.WriteLine("###### La commande a pour ID"+id);
                for (int i=0; i< quantitesCommandees.Length; i++)
                    {
                        int qte;
                        int.TryParse(quantitesCommandees[i],out qte);
                        if (qte>0)
                        {
                            LigneCommande A = new LigneCommande();
                            A.ArticleID=Articles[i].ID;
                        A.CommandeID = id;
                            A.quantite=qte;
                            A.prix = Articles[i].prixU * qte;
                            _context.LigneCommandes.Add(A);
                            Articles[i].quantite-=qte;
                            _context.Attach(Articles[i]).State = EntityState.Modified;
                        }
                    
                }
                    Table table= _context.Tables.FirstOrDefault(t => t.ID == commande.tableID);
                    table.occuppe = true;
                    _context.Attach(table).State = EntityState.Modified;
                
                     await _context.SaveChangesAsync();
                 
                
                await _hubContext.Clients.All.SendAsync("RecevoirCommande");
            }
                return RedirectToPage("./order");

            }
        }
    }
