using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetASI.Models
{
  public class Facture
    {
       public int ID { get; set; }
       
        public int CommandeID { get; set; } 
       
        public Commande commande { get; set; }
    }
}
