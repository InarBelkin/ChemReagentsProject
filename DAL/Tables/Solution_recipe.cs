using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class Solution_recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GOST { get; set; }
        public virtual List<Concentration> Concentrations { get; set; }
    }
}
