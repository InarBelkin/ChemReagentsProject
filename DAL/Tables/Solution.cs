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

        //public int? SolutionRecipeId { get; set; }
        public int? ConcentrationId {get;set;}
        [ForeignKey("ConcentrationId")]
        public Concentration Concentration { get; set; }
        public DateTime Date_Begin { get; set; }
        public virtual List<Solution_line> Solution_Lines { get; set; }
    }
}
