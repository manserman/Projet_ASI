using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Caissier.Data
{
    public class Article
    {
        public int ID { get; set; }
      
        public  string nom { get; set; }
       public int prixU { get; set; }
       

        public int quantite { get; set; }
    }
}
