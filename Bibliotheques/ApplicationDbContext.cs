using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ProjetBAR;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<Serveur> Serveur { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<LigneCommande> LigneCommandes {get; set;}
    }
}
