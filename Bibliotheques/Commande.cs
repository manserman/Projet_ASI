using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataModels
{
    public class Commande
    {
        int numero { get; set; }
        public Boolean validee { get; set; }
        public int tableID { get; set; }
        public Table? table { get; set; }
        public Boolean servie { get; set; }
        public DateTime datecomm { get; set; }
    }
}
