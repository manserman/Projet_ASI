using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjetASI.Models
{
    public class Article
    {
        public int ID { get; set; }

        public string nom { get; set; }
        public double prixU { get; set; }


        public int quantite { get; set; }
    }
}
