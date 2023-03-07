using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class DBContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

           
        }
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<Serveur> Serveur { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<LigneCommande> LigneCommandes {get; set;}

        public DbSet<Facture> Factures { get; set; }

    }
}
