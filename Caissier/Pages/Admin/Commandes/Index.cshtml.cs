﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Data;
using ProjetASI.Models;

namespace ProjetASI.Pages.Admin.Commandes
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ProjetASI.Data.DBContext _context;

        public IndexModel(ProjetASI.Data.DBContext context)
        {
            _context = context;
        }

        public IList<Commande> Commande { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Commandes != null)
            {
                Commande = await _context.Commandes
                .Include(c => c.serveur)
                .Include(c => c.table).ToListAsync();
            }
        }
    }
}
