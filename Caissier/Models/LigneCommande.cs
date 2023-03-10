using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetASI.Models
{
    public class LigneCommande
    {
       public int ID { get;set; }

       public int ArticleID { get;set; } 
        public Article article{ get;set; }

        public int CommandeID { get;set; }

        public Commande commande{ get;set; }
        public int quantite { get; set; }
        public int prix { get; set; }

    }
}
