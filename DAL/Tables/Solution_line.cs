using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class Solution_line
    {
        public int Id { get; set; }

        public int SolutionId { get; set; }
        [ForeignKey("SolutionId")]
        public Solution Solution { get; set; }

        public int SupplyId { get; set; }
        [ForeignKey("SupplyId")]
        public Supply Supply { get; set; }

        public float Count { get; set; }
    }
}
