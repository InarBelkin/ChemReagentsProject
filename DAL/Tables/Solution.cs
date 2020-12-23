using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class Solution
    {
        public int Id { get; set; }

        public int? SolutionRecipeId { get; set; }
        [ForeignKey("SolutionRecipeId")]
        public Solution_recipe Solution_Recipe { get; set; }
        public DateTime Date_Begin { get; set; }
    }
}
