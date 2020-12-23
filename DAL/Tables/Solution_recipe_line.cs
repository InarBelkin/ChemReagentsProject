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
        
        public int ConcentracionId { get; set; }
        [ForeignKey("ConcentracionId")]
        public Concentration Сoncentration { get; set; }

        public float Count { get; set; }
    }
}
