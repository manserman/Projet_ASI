﻿using ProjetASI.Data;
using ProjetASI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Collections.Generic;

namespace ProjetASI.Pages.Order
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
       
        [BindProperty]
        public IList<Table> TablesOccupeessanscommandes { get; set; }
        [BindProperty]
        public Article Article { get; set; }
        [BindProperty]
        public int prixt { get;  set; }
        [BindProperty]
        public  Commande commande { get; set; }
        public CommanderModel(DBContext context)
        {
            _context = context;
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
                Console.WriteLine(commande.tableID);
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


            }
            return RedirectToPage("./Index");

        }
    }
}
