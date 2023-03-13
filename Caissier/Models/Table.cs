using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetASI.Models
{
    public class Table
    {
        public int ID { get; set; }
        [Required]
        public Boolean occuppe { get; set; }

      
        
    }
}
