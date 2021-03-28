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

        public int? SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }

        public string Manufacturer { get; set; }

        public int? ReportId { get; set; }
        [ForeignKey("ReportId")]
        public Report Report { get; set; }

        public string IncomContr { get; set; }
        public string Qualification { get; set; }


        public DateTime Date_Production { get; set; }
        public DateTime Date_StartUse { get; set; }
        public DateTime Date_Expiration { get; set; }
        public DateTime Date_UnWrite { get;set; }

        public decimal Density { get; set; }
        public bool Active { get; set; }
        public decimal Count { get; set; }


        public virtual List<Solution_line> Solution_Lines { get; set; }
        public virtual List<Supply_consumption> Consumptions { get; set; }
    }
}
  