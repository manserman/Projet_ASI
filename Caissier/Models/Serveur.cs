using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ProjetASI.Models
{
    public class Serveur
    {
        public int ID { get; set; }
        public string nom { get; set; }

        public ICollection<Commande>? Commandes { get; set; }

        public string UserID { get; set; } 

       
    }
}
