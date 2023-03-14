using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ProjetASI.Models
{
    public class Commande
    {   public int  ID {get; set;}
       
        public Boolean? validee { get; set; }
        public int tableID { get; set; }
        public Table? table { get; set; }
        
        public DateTime datecomm { get; set; }
        [Display(Name = "Articles commandees")]
        public ICollection<LigneCommande>? Articles { get; set; }
        public Boolean? isServed { get; set; }
        public int serveurId { get; set; }
        public Serveur serveur { get; set;}
        public Boolean ?commencer { get; set; }
        public Boolean? isPaid { get; set; }
        public double prix { get; set; }
    }
}
