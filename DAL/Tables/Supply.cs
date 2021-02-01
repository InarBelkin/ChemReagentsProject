using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class Supply
    {
        public int Id { get; set; }

        public int ReagentId { get; set; }
        [ForeignKey("ReagentId")]
        public Reagent Reagent { get; set; }

        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }

        public DateTime Date_Begin { get; set; }
        public DateTime Date_End { get; set; }
        public byte State { get; set; }
        public bool Unpacked { get; set; }
        public float Count { get; set; }

        public virtual List<Solution_line> Solution_Lines { get; set; }
        public virtual List<Supply_consumption> Consumptions { get; set; }
    }
}
