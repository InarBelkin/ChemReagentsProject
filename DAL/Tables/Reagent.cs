using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class Reagent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public string units { get; set; }
        public bool isWater { get; set; }
        public decimal density { get; set; }

        public virtual List<Supply> Supplies { get; set; }
        //public virtual List<Solution_recipe_line> SolutRecipeLines { get; set; }

    }
}
