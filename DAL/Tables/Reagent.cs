using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class Reagent
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }

      
        public bool isWater { get; set; }
        public bool isAlwaysWater { get; set; }
        public bool IsAccounted { get; set; }


        public virtual List<Supply> Supplies { get; set; }
        //public virtual List<Solution_recipe_line> SolutRecipeLines { get; set; }

    }
}
