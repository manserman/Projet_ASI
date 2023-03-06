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
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = ProjetBAR; Trusted_Connection = True; MultipleActiveResultSets = true");
            }
        }
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<Serveur> Serveur { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<LigneCommande> LigneCommandes {get; set;}
    }
}
