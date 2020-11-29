using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class Solution_recipe_line
    {
        public int Id { get; set; }

        public int ReagentId { get; set; }
        [ForeignKey("ReagentId")]
        public Reagent Reagent { get; set; }
        
        public int SolutionRecipeId { get; set; }
        [ForeignKey("SolutionRecipeId")]
        public Solution_recipe Solution_Recipe { get; set; }

        public float Count { get; set; }
    }
}
